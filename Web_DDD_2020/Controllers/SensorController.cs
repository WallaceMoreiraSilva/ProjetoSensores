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
using Microsoft.Extensions.Logging;
using SensoresAPP.ViewModels;

namespace ProjetoDDD.Controllers
{
    public class SensorController : Controller
    {
        private readonly ISensorService _InterfaceSensorService;
        private readonly IPaisService _InterfacePaisService;
        private readonly IRegiaoService _InterfaceRegiaoService;       
        private readonly IStatusSensorService _InterfaceStatusSensorService;
        private readonly ILogger<SensorController> _logger;

        public SensorController( ISensorService InterfaceSensorService, 
                                 IPaisService InterfacePaisService, 
                                 IRegiaoService InterfaceRegiaoService,
                                 IStatusSensorService InterfaceStatusSensorService,
                                 ILogger<SensorController> logger
                                ) 
        {
            _InterfaceSensorService = InterfaceSensorService;
            _InterfacePaisService = InterfacePaisService;
            _InterfaceRegiaoService = InterfaceRegiaoService;
            _InterfaceStatusSensorService = InterfaceStatusSensorService;
            _logger = logger;
        }
        
        public async Task<IActionResult> Index()
        {            
            List<SensorViewModel> lista = new List<SensorViewModel>();
            
            try
            {                
                var entitySensor = await _InterfaceSensorService.List();

                if (entitySensor != null)
                {       
                    foreach (var item in entitySensor)
                    {
                        var nomePais = _InterfacePaisService.GetEntityById(item.PaisId).Result.Nome;
                        var nomeRegiao = _InterfaceRegiaoService.GetEntityById(item.RegiaoId).Result.Nome;
                        var statusSensor = _InterfaceStatusSensorService.GetEntityById(item.StatusSensorId).Result.Nome;

                        lista.Add(new SensorViewModel
                        {
                            Id = item.Id,
                            Nome = item.Nome,
                            Numero = item.Numero,
                            DataCadastro = item.DataCadastro,
                            DataAlteracao = item.DataAlteracao,
                            PaisId = item.PaisId,
                            NomePais = nomePais,
                            RegiaoId = item.RegiaoId,
                            NomeRegiao = nomeRegiao,
                            StatusSensorId = item.StatusSensorId,
                            StatusSensor = statusSensor
                        });
                    }
                }                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }            
           
            return View(lista);
        }        
       
        public IActionResult Create()
        {    
            var listaDePaises = _InterfacePaisService.List();
            var listaDeRegioes = _InterfaceRegiaoService.List();

            List<Pais> paises = new List<Pais>();
            List<Regiao> regioes = new List<Regiao>();

            if(listaDePaises != null && listaDeRegioes != null)
            {
                foreach (var item in listaDePaises.Result.AsEnumerable())
                {
                    paises.Add(new Pais
                    {
                        Id = item.Id,
                        Nome = item.Nome,
                        IsoDuasLetras = item.IsoDuasLetras,
                        IsoTresLetras = item.IsoTresLetras,
                        NumeroCodigoIso = item.NumeroCodigoIso,
                        DataCadastro = item.DataCadastro,
                        DataAlteracao = item.DataAlteracao
                    }); ;
                }

                foreach (var item in listaDeRegioes.Result.AsEnumerable())
                {
                    regioes.Add(new Regiao
                    {
                        Id = item.Id,
                        Nome = item.Nome,                   
                        DataCadastro = item.DataCadastro,
                        DataAlteracao = item.DataAlteracao
                    });
                }
            }
            
            ViewBag.Paises = paises.ToList().Select(c => new SelectListItem(){ Value = c.Id.ToString(), Text = c.Nome.ToString()}).ToList();

            ViewBag.Regioes = regioes.ToList().Select(c => new SelectListItem() { Value = c.Id.ToString(), Text = c.Nome.ToString() }).ToList();

            return View();
        }        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SensorViewModel sensorViewModel)
        {    
            if (ModelState.IsValid)
            {
                Sensor sensor = new Sensor();
                               
                sensor.Nome = sensorViewModel.Nome;
                sensor.Numero = sensorViewModel.Numero;
                sensor.PaisId = sensorViewModel.PaisId;
                sensor.RegiaoId = sensorViewModel.RegiaoId;
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
            var sensor = await _InterfaceSensorService.GetEntityById((int)id);

            SensorViewModel sensorViewModel;

            try
            {  
                if (id == null || sensor == null)
                    return NotFound();   

                bool status = sensor.StatusSensorId == (int)StatusSensorEnum.Ativo ? true : false;   

                var listaDePaises = _InterfacePaisService.List();
                var listaDeRegioes = _InterfaceRegiaoService.List();

                List<Pais> paises = new List<Pais>();
                List<Regiao> regioes = new List<Regiao>();

                if (listaDePaises != null && listaDeRegioes != null)
                {
                    foreach (var item in listaDePaises.Result.AsEnumerable())
                    {
                        paises.Add(new Pais
                        {
                            Id = item.Id,
                            Nome = item.Nome,
                            IsoDuasLetras = item.IsoDuasLetras,
                            IsoTresLetras = item.IsoTresLetras,
                            NumeroCodigoIso = item.NumeroCodigoIso,
                            DataCadastro = item.DataCadastro,
                            DataAlteracao = item.DataAlteracao
                        }); ;
                    }

                    foreach (var item in listaDeRegioes.Result.AsEnumerable())
                    {
                        regioes.Add(new Regiao
                        {
                            Id = item.Id,
                            Nome = item.Nome,
                            DataCadastro = item.DataCadastro,
                            DataAlteracao = item.DataAlteracao
                        });
                    }
                }

                ViewBag.Paises = paises.ToList().Select(c => new SelectListItem() { Value = c.Id.ToString(), Text = c.Nome.ToString() }).ToList();

                ViewBag.Regioes = regioes.ToList().Select(c => new SelectListItem() { Value = c.Id.ToString(), Text = c.Nome.ToString() }).ToList(); 

                sensorViewModel = new SensorViewModel
                {
                    Ativo = status,
                    Id = sensor.Id,
                    Nome = sensor.Nome,
                    Numero = sensor.Numero,
                    PaisId = sensor.PaisId,
                    RegiaoId = sensor.RegiaoId,
                    DataCadastro = sensor.DataCadastro,
                    DataAlteracao = sensor.DataAlteracao
                };

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
            Sensor sensor = new Sensor();

            if (id != sensorViewModel.Id)           
                return NotFound();            

            if (ModelState.IsValid)
            {
                try
                {      
                    sensor.Nome = sensorViewModel.Nome;
                    sensor.Numero = sensorViewModel.Numero;
                    sensor.PaisId = sensorViewModel.PaisId;
                    sensor.RegiaoId = sensorViewModel.RegiaoId;                   
                    sensor.DataAlteracao = DateTime.Now;
                    sensor.StatusSensorId = sensorViewModel.Ativo == true ? (int)StatusSensorEnum.Ativo : (int)StatusSensorEnum.Inativo;

                    await _InterfaceSensorService.Update(sensor);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await SensorExists(sensor.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(sensorViewModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var sensor = await _InterfaceSensorService.GetEntityById((int)id);
            var nomePais = _InterfacePaisService.GetEntityById(sensor.PaisId).Result.Nome;
            var nomeRegiao = _InterfaceRegiaoService.GetEntityById(sensor.RegiaoId).Result.Nome;

            SensorViewModel sensorViewModel;

            try
            {          
                if (id == null || sensor == null)
                    return NotFound();     

                bool status = sensor.StatusSensorId == (int)StatusSensorEnum.Ativo ? true : false;

                sensorViewModel = new SensorViewModel
                {
                    Ativo = status,
                    Id = sensor.Id,
                    Nome = sensor.Nome,
                    Numero = sensor.Numero,
                    PaisId = sensor.PaisId,
                    NomePais = nomePais,
                    RegiaoId = sensor.RegiaoId,
                    NomeRegiao = nomeRegiao,
                    DataCadastro = sensor.DataCadastro,
                    DataAlteracao = sensor.DataAlteracao
                };
              
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
            var sensor = await _InterfaceSensorService.GetEntityById((int)id);
            var nomePais = _InterfacePaisService.GetEntityById(sensor.PaisId).Result.Nome;
            var nomeRegiao = _InterfaceRegiaoService.GetEntityById(sensor.RegiaoId).Result.Nome;

            SensorViewModel sensorViewModel;

            try
            {      
                if (id == null || sensor == null)
                    return NotFound();

                bool status = sensor.StatusSensorId == (int)StatusSensorEnum.Ativo ? true : false;

                sensorViewModel = new SensorViewModel
                {
                    Ativo = status,
                    Id = sensor.Id,
                    Nome = sensor.Nome,
                    Numero = sensor.Numero,
                    PaisId = sensor.PaisId,
                    NomePais = nomePais,
                    RegiaoId = sensor.RegiaoId,
                    NomeRegiao = nomeRegiao,
                    DataCadastro = sensor.DataCadastro,
                    DataAlteracao = sensor.DataAlteracao
                };

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }              

            return View(sensorViewModel);
        }

        private async Task<bool> SensorExists(int id)
        {
            var objeto = await _InterfaceSensorService.GetEntityById(id);

            return objeto != null;
        }
    }
}
