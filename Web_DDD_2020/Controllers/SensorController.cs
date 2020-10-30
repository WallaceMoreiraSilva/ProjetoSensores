using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using SensoresAPP.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System;
using Enum;
using SensoresAPP.ViewModels;
using AutoMapper;
using ProjetoDDD.Models;
using System.Diagnostics;

namespace ProjetoDDD.Controllers
{
    public class SensorController : Controller
    {
        private readonly ISensorService _InterfaceSensorService;
        private readonly IPaisService _InterfacePaisService;
        private readonly IRegiaoService _InterfaceRegiaoService;       
        private readonly IStatusSensorService _InterfaceStatusSensorService;
        private readonly ILogAuditoriaService _InterfaceLogAuditoriaService;
        private readonly IMapper _mapper;    

        public SensorController( ISensorService InterfaceSensorService, 
                                 IPaisService InterfacePaisService, 
                                 IRegiaoService InterfaceRegiaoService,
                                 IStatusSensorService InterfaceStatusSensorService,
                                 ILogAuditoriaService InterfaceLogAuditoriaService,
                                 IMapper mapper                                 
                                ) 
        {
            _InterfaceSensorService = InterfaceSensorService;
            _InterfacePaisService = InterfacePaisService;
            _InterfaceRegiaoService = InterfaceRegiaoService;
            _InterfaceStatusSensorService = InterfaceStatusSensorService;   
            _InterfaceLogAuditoriaService = InterfaceLogAuditoriaService;
            _mapper = mapper;
        }     

        public async Task<IActionResult> Index()
        {
            List<SensorViewModel> sensoresViemModel = new List<SensorViewModel>();    

            try
            {
                IEnumerable<Sensor> _sensor = await _InterfaceSensorService.List();

                sensoresViemModel = _mapper.Map<List<SensorViewModel>>(_sensor);

                if (sensoresViemModel != null)
                {
                    foreach (var item in sensoresViemModel)
                    {
                        var nomePais = _InterfacePaisService.GetEntityById(item.PaisId).Result.Nome;
                        var nomeRegiao = _InterfaceRegiaoService.GetEntityById(item.RegiaoId).Result.Nome;
                        var statusSensor = _InterfaceStatusSensorService.GetEntityById(item.StatusSensorId).Result.Nome;

                        item.NomePais = nomePais;
                        item.NomeRegiao = nomeRegiao;
                        item.StatusDoSensor = statusSensor;
                    }
                }               
            }
            catch (Exception ex)
            {
                string mensagem = string.Format("{0} - {1}{2}{3}", DateTime.Now, ex.Message, ex.InnerException, ex.StackTrace);

                await _InterfaceLogAuditoriaService.Add(new LogAuditoria
                {
                    DetalhesAuditoria = mensagem
                });

                ViewBag.Error = "Não foi possível listar os sensores.";
            }

            return View(sensoresViemModel);
        }

        public async Task<IActionResult> Create()
        {
            List<PaisViewModel> paises = new List<PaisViewModel>();
            List<RegiaoViewModel> regioes = new List<RegiaoViewModel>();

            try
            {
                IEnumerable<Pais> _paises = await _InterfacePaisService.List();
                IEnumerable<Regiao> _regioes = await _InterfaceRegiaoService.List();

                paises = _mapper.Map<List<PaisViewModel>>(_paises);
                regioes = _mapper.Map<List<RegiaoViewModel>>(_regioes);

                ViewBag.Paises = paises.Select(c => new SelectListItem() { Value = c.Id.ToString(), Text = c.Nome.ToString() });
                ViewBag.Regioes = regioes.Select(c => new SelectListItem() { Value = c.Id.ToString(), Text = c.Nome.ToString() });
            }
            catch (Exception ex)
            {
                string mensagem = string.Format("{0} - {1}{2}{3}", DateTime.Now, ex.Message, ex.InnerException, ex.StackTrace);

                await _InterfaceLogAuditoriaService.Add(new LogAuditoria
                {
                    DetalhesAuditoria = mensagem
                });

                TempData["ErroMessage"] = "Houve um erro inesperado ao tentar realizar o cadastro. Por favor entre em contato com o suporte.";

                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SensorViewModel sensorViewModel)
        {            
            try
            {
                if (ModelState.IsValid)
                {
                    Sensor sensor = _mapper.Map<Sensor>(sensorViewModel);
                    sensor.DataCadastro = DateTime.Now;
                    sensor.DataAlteracao = DateTime.Now;
                    sensor.StatusSensorId = sensorViewModel.Ativo == true ? (int)StatusSensorEnum.Ativo : (int)StatusSensorEnum.Inativo;                    

                    await _InterfaceSensorService.Add(sensor);

                    string mensagem = string.Format("{0} - O sensor {1} foi criado com sucesso", DateTime.Now, sensor.Id.ToString());

                    await _InterfaceLogAuditoriaService.Add(new LogAuditoria
                    {
                        DetalhesAuditoria = mensagem
                    });                   
                }
            }
            catch (Exception ex)
            {
                string mensagem = string.Format("{0} - {1}{2}{3}", DateTime.Now, ex.Message, ex.InnerException, ex.StackTrace);

                await _InterfaceLogAuditoriaService.Add(new LogAuditoria
                {
                    DetalhesAuditoria = mensagem
                });
               
                TempData["ErroMessage"] = "Não foi possível realizar o cadastro. Tente novamente.";                

                return RedirectToAction(nameof(Create));
            } 

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            List<PaisViewModel> paises = new List<PaisViewModel>();
            List<RegiaoViewModel> regioes = new List<RegiaoViewModel>();
            SensorViewModel sensorViewModel = new SensorViewModel();

            try
            {

                var sensor = await _InterfaceSensorService.GetEntityById((int)id);
                bool status = sensor.StatusSensorId == (int)StatusSensorEnum.Ativo ? true : false;

                if (id == null)               
                    throw new Exception(string.Format("{0} - {1} - {2}", DateTime.Now, id, NotFound()));
                                
                if (sensor == null)              
                    throw new Exception(string.Format("{0} - {1} - {2}", DateTime.Now, sensor.Id, NotFound()));

                IEnumerable<Pais> _paises = await _InterfacePaisService.List();
                IEnumerable<Regiao> _regioes = await _InterfaceRegiaoService.List();

                paises = _mapper.Map<List<PaisViewModel>>(_paises);
                regioes = _mapper.Map<List<RegiaoViewModel>>(_regioes);

                ViewBag.Paises = paises.Select(c => new SelectListItem() { Value = c.Id.ToString(), Text = c.Nome.ToString() });
                ViewBag.Regioes = regioes.Select(c => new SelectListItem() { Value = c.Id.ToString(), Text = c.Nome.ToString() });                

                sensorViewModel = _mapper.Map<SensorViewModel>(sensor);
                sensorViewModel.Ativo = status;

            }
            catch (Exception ex)
            {
                string mensagem = string.Format("{0} - {1}{2}{3}", DateTime.Now, ex.Message, ex.InnerException, ex.StackTrace);

                await _InterfaceLogAuditoriaService.Add(new LogAuditoria
                {
                    DetalhesAuditoria = mensagem
                });

                TempData["ErroMessage"] = "Houve um erro inesperado ao tentar editar o sensor. Por favor entre em contato com o suporte.";

                return RedirectToAction(nameof(Index));
            }

            return View(sensorViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SensorViewModel sensorViewModel)
        {      
            if (ModelState.IsValid)
            {
                Sensor sensor = new Sensor();
                string mensagem = string.Empty;

                try
                {    
                    if (id != sensorViewModel.Id)                  
                        throw new Exception(string.Format("{0} - O id: " + id + "diferente do: " + sensorViewModel.Id, DateTime.Now));                    

                    sensor = _mapper.Map<Sensor>(sensorViewModel); 
                    sensor.DataAlteracao = DateTime.Now;
                    sensor.StatusSensorId = sensorViewModel.Ativo == true ? (int)StatusSensorEnum.Ativo : (int)StatusSensorEnum.Inativo;

                    await _InterfaceSensorService.Update(sensor);

                    mensagem = string.Format("{0} - O sensor {1} foi alterado com sucesso", DateTime.Now, sensor.Id.ToString());

                    await _InterfaceLogAuditoriaService.Add(new LogAuditoria
                    {
                        DetalhesAuditoria = mensagem
                    });
                }
                catch (DbUpdateConcurrencyException ex)
                {             
                    if (!await SensorExists(sensor.Id))                    
                        mensagem = string.Format("{0} - {1}", DateTime.Now, NotFound());   
                    else                    
                        mensagem = string.Format("{0} - {1}{2}{3}", DateTime.Now, ex.Message, ex.InnerException, ex.StackTrace);                   

                    await _InterfaceLogAuditoriaService.Add(new LogAuditoria
                    {
                        DetalhesAuditoria = mensagem
                    });

                    ViewBag.Error = "Não foi possível editar o sensor.";

                    return View(sensorViewModel);
                }
            }           

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            SensorViewModel sensorViewModel = new SensorViewModel();

            try
            {
                var sensor = await _InterfaceSensorService.GetEntityById((int)id);                

                if (id == null)
                    throw new Exception(string.Format("{0} - {1} - {2}", DateTime.Now, id, NotFound()));

                if (sensor == null)
                    throw new Exception(string.Format("{0} - {1} - {2}", DateTime.Now, sensor.Id, NotFound()));

                var nomePais = _InterfacePaisService.GetEntityById(sensor.PaisId).Result.Nome;
                var nomeRegiao = _InterfaceRegiaoService.GetEntityById(sensor.RegiaoId).Result.Nome;

                bool status = sensor.StatusSensorId == (int)StatusSensorEnum.Ativo ? true : false;               

                sensorViewModel = _mapper.Map<SensorViewModel>(sensor);
                sensorViewModel.Ativo = status;
                sensorViewModel.NomePais = nomePais;
                sensorViewModel.NomeRegiao = nomeRegiao;
            }
            catch (Exception ex)
            {
                string mensagem = string.Format("{0} - {1}{2}{3}", DateTime.Now, ex.Message, ex.InnerException, ex.StackTrace);

                await _InterfaceLogAuditoriaService.Add(new LogAuditoria
                {
                    DetalhesAuditoria = mensagem
                });

                TempData["ErroMessage"] = "Houve um erro inesperado ao tentar deletar o sensor. Por favor entre em contato com o suporte.";

                return RedirectToAction(nameof(Index));
            }

            return View(sensorViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var sensor = await _InterfaceSensorService.GetEntityById(id);

                await _InterfaceSensorService.Delete(sensor);

                string mensagem = string.Format("{0} - O sensor {1} foi deletado com sucesso", DateTime.Now, sensor.Id.ToString());

                await _InterfaceLogAuditoriaService.Add(new LogAuditoria
                {
                    DetalhesAuditoria = mensagem
                });
            }
            catch (Exception ex)
            {
                string mensagem = string.Format("{0} - {1}{2}{3}", DateTime.Now, ex.Message, ex.InnerException, ex.StackTrace);

                await _InterfaceLogAuditoriaService.Add(new LogAuditoria
                {
                    DetalhesAuditoria = mensagem
                });

                TempData["ErroMessage"] = "Houve um erro inesperado ao tentar deletar o sensor. Por favor entre em contato com o suporte.";                
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            SensorViewModel sensorViewModel = new SensorViewModel();

            try
            {
                var sensor = await _InterfaceSensorService.GetEntityById((int)id);               

                if (id == null)
                    throw new Exception(string.Format("{0} - {1} - {2}", DateTime.Now, id, NotFound()));

                if (sensor == null)
                    throw new Exception(string.Format("{0} - {1} - {2}", DateTime.Now, sensor.Id, NotFound()));

                var nomePais = _InterfacePaisService.GetEntityById(sensor.PaisId).Result.Nome;
                var nomeRegiao = _InterfaceRegiaoService.GetEntityById(sensor.RegiaoId).Result.Nome;

                bool status = sensor.StatusSensorId == (int)StatusSensorEnum.Ativo ? true : false;

                sensorViewModel = _mapper.Map<SensorViewModel>(sensor);
                sensorViewModel.Ativo = status;
                sensorViewModel.NomePais = nomePais;
                sensorViewModel.NomeRegiao = nomeRegiao;
            }
            catch (Exception ex)
            {
                string mensagem = string.Format("{0} - {1}{2}{3}", DateTime.Now, ex.Message, ex.InnerException, ex.StackTrace);

                await _InterfaceLogAuditoriaService.Add(new LogAuditoria
                {
                    DetalhesAuditoria = mensagem
                });

                TempData["ErroMessage"] = "Houve um erro inesperado ao tentar exibir os detalhes do sensor. Por favor entre em contato com o suporte.";

                return RedirectToAction(nameof(Index));
            }

            return View(sensorViewModel);
        }

        private async Task<bool> SensorExists(int id)
        {
            Sensor sensor = new Sensor();
            bool sensorExiste;

            try
            {
                sensor = await _InterfaceSensorService.GetEntityById(id);
                sensorExiste = sensor != null ? true : false;
            }
            catch (Exception ex)
            {
                string mensagem = string.Format("{0} - {1}{2}{3}", DateTime.Now, ex.Message, ex.InnerException, ex.StackTrace);
                sensorExiste = false;                

                await _InterfaceLogAuditoriaService.Add(new LogAuditoria
                {
                    DetalhesAuditoria = mensagem
                });                
            }

            return sensorExiste;
        }
       
    }
}
