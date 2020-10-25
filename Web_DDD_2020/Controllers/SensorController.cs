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
        private readonly IMapper _mapper;


        public SensorController( ISensorService InterfaceSensorService, 
                                 IPaisService InterfacePaisService, 
                                 IRegiaoService InterfaceRegiaoService,
                                 IStatusSensorService InterfaceStatusSensorService,                                
                                 IMapper mapper
                                ) 
        {
            _InterfaceSensorService = InterfaceSensorService;
            _InterfacePaisService = InterfacePaisService;
            _InterfaceRegiaoService = InterfaceRegiaoService;
            _InterfaceStatusSensorService = InterfaceStatusSensorService;            
            _mapper = mapper;
        }     

        public async Task<IActionResult> Index()
        {
            List<SensorViewModel> sensores = new List<SensorViewModel>();    

            try
            {
                IEnumerable<Sensor> _sensor = await _InterfaceSensorService.List();

                sensores = _mapper.Map<List<SensorViewModel>>(_sensor);

                if (sensores != null)
                {
                    foreach (var item in sensores)
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
                throw new Exception(ex.Message);
            }            

            return View(sensores);
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
                throw new Exception(ex.Message);
            }            

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SensorViewModel sensorViewModel)
        {
            if (ModelState.IsValid)
            {
                Sensor sensor = _mapper.Map<Sensor>(sensorViewModel);                
                sensor.DataCadastro = DateTime.Now;
                sensor.DataAlteracao = DateTime.Now;
                sensor.StatusSensorId = sensorViewModel.Ativo == true ? (int)StatusSensorEnum.Ativo : (int)StatusSensorEnum.Inativo;

                await _InterfaceSensorService.Add(sensor);                

                return RedirectToAction(nameof(Index));
            }

            return View(sensorViewModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            List<PaisViewModel> paises = new List<PaisViewModel>();
            List<RegiaoViewModel> regioes = new List<RegiaoViewModel>();
            SensorViewModel sensorViewModel = new SensorViewModel();

            try
            {
                var sensor = await _InterfaceSensorService.GetEntityById((int)id);

                if (id == null || sensor == null)
                    return NotFound();

                bool status = sensor.StatusSensorId == (int)StatusSensorEnum.Ativo ? true : false;

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
                throw new Exception(ex.Message);
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

                if (id != sensorViewModel.Id)
                    return NotFound();

                try
                {
                    sensor = _mapper.Map<Sensor>(sensorViewModel); 
                    sensor.DataAlteracao = DateTime.Now;
                    sensor.StatusSensorId = sensorViewModel.Ativo == true ? (int)StatusSensorEnum.Ativo : (int)StatusSensorEnum.Inativo;

                    await _InterfaceSensorService.Update(sensor);                  
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!await SensorExists(sensor.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw new Exception(ex.Message);
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(sensorViewModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            SensorViewModel sensorViewModel = new SensorViewModel();

            try
            {
                var sensor = await _InterfaceSensorService.GetEntityById((int)id);
                var nomePais = _InterfacePaisService.GetEntityById(sensor.PaisId).Result.Nome;
                var nomeRegiao = _InterfaceRegiaoService.GetEntityById(sensor.RegiaoId).Result.Nome;

                if (id == null || sensor == null)
                    return NotFound();

                bool status = sensor.StatusSensorId == (int)StatusSensorEnum.Ativo ? true : false;               

                sensorViewModel = _mapper.Map<SensorViewModel>(sensor);
                sensorViewModel.Ativo = status;
                sensorViewModel.NomePais = nomePais;
                sensorViewModel.NomeRegiao = nomeRegiao;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            SensorViewModel sensorViewModel = new SensorViewModel();

            try
            {
                var sensor = await _InterfaceSensorService.GetEntityById((int)id);
                var nomePais = _InterfacePaisService.GetEntityById(sensor.PaisId).Result.Nome;
                var nomeRegiao = _InterfaceRegiaoService.GetEntityById(sensor.RegiaoId).Result.Nome;

                if (id == null || sensor == null)
                    return NotFound();

                bool status = sensor.StatusSensorId == (int)StatusSensorEnum.Ativo ? true : false;

                sensorViewModel = _mapper.Map<SensorViewModel>(sensor);
                sensorViewModel.Ativo = status;
                sensorViewModel.NomePais = nomePais;
                sensorViewModel.NomeRegiao = nomeRegiao;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
                throw new Exception(ex.Message);
            }

            return sensorExiste;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
