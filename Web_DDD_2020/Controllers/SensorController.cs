using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoDDD.Sensores.Domain.Entities;
using ProjetoDDD.Sensores.Application.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System;
using ProjetoDDD.Sensores.Infra.CrossCutting.Enum;
using ProjetoDDD.Sensores.Application.ViewModel;
using AutoMapper;

namespace ProjetoDDD.Sensores.Presentation.Controllers
{
    public class SensorController : Controller
    {
        private readonly ISensorService _InterfaceSensorService;
        private readonly IPaisService _InterfacePaisService;
        private readonly IRegiaoService _InterfaceRegiaoService;       
        private readonly IMapper _mapper;      

        public SensorController
        ( 
            ISensorService InterfaceSensorService, 
            IPaisService InterfacePaisService, 
            IRegiaoService InterfaceRegiaoService,
            IMapper mapper,
            ILogComIdentificadorUnico logger
        ) 
        {
            _InterfaceSensorService = InterfaceSensorService;
            _InterfacePaisService = InterfacePaisService;
            _InterfaceRegiaoService = InterfaceRegiaoService;
            _logger = logger;
            _mapper = mapper;            
        }

        public async Task<IActionResult> Index()
        {
            ILogComIdentificadorUnico log = _logger.CriarLog(gestaolog => gestaolog.GetLogger(this.GetType().Name));  
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

                        item.StatusSensor = Convert.ToInt32(item.StatusSensor) == (int)StatusSensorEnum.Ativo ? StatusSensorEnum.Ativo.ToString() : StatusSensorEnum.Inativo.ToString();
                        item.NomeRegiao = nomeRegiao;
                        item.NomePais = nomePais;                                        
                    }
                }
                log.Informacao("Listagem de sensores realizado com sucesso");
            }
            catch (Exception ex)
            {
                string mensagem = string.Format("{0} - {1}{2}{3}", DateTime.Now, ex.Message, ex.InnerException, ex.StackTrace);               
               
                log.Erro("Houve um erro ao tentar listar os sensores", mensagem);

                ViewBag.Error = "Houve um erro ao tentar listar os sensores. Por favor entre em contato com o suporte.";
            }

            return View(sensoresViemModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ILogComIdentificadorUnico log = _logger.CriarLog(gestaolog => gestaolog.GetLogger(this.GetType().Name));

            try
            {             
                List<PaisViewModel> listarPaises = await ListarPaises();
                List<RegiaoViewModel> listarRegioes = await ListarRegioes();                

                ViewBag.Paises = listarPaises.Select(c => new SelectListItem() { Value = c.Id.ToString(), Text = c.Nome.ToString() });
                ViewBag.Regioes = listarRegioes.Select(c => new SelectListItem() { Value = c.Id.ToString(), Text = c.Nome.ToString() });               
            }
            catch (Exception ex)
            {
                string mensagem = string.Format("{0} - {1}{2}{3}", DateTime.Now, ex.Message, ex.InnerException, ex.StackTrace);

                log.Erro("Houve um erro ao tentar abrir a tela para criar um sensor", mensagem);

                TempData["ErroMessage"] = "Houve um erro inesperado ao tentar realizar o cadastro. Por favor entre em contato com o suporte.";

                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SensorViewModel sensorViewModel)
        {
            ILogComIdentificadorUnico log = _logger.CriarLog(gestaolog => gestaolog.GetLogger(this.GetType().Name));
            string mensagem = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    Sensor sensor = _mapper.Map<Sensor>(sensorViewModel);
                    sensor.DataCadastro = DateTime.Now;
                    sensor.DataAlteracao = DateTime.Now;
                    sensor.StatusSensor = sensorViewModel.Ativo == true ? (int)StatusSensorEnum.Ativo : (int)StatusSensorEnum.Inativo;

                    await _InterfaceSensorService.Add(sensor);                   
                    
                    log.Informacao("O sensor {0} foi criado com sucesso", sensor.Id);
                }
            }
            catch (Exception ex)
            {
                mensagem = string.Format("{0} - {1}{2}{3}", DateTime.Now, ex.Message, ex.InnerException, ex.StackTrace);              

                log.Erro("Houve um erro ao tentar criar o Sensor", mensagem);             

                List<PaisViewModel> listarPaises = await ListarPaises();
                List<RegiaoViewModel> listarRegioes = await ListarRegioes();

                ViewBag.Paises = listarPaises.Select(c => new SelectListItem() { Value = c.Id.ToString(), Text = c.Nome.ToString() });
                ViewBag.Regioes = listarRegioes.Select(c => new SelectListItem() { Value = c.Id.ToString(), Text = c.Nome.ToString() });

                ViewBag.Error = "Houve um erro ao tentar criar o sensores. Por favor tente novamente.";

                return View(sensorViewModel);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            ILogComIdentificadorUnico log = _logger.CriarLog(gestaolog => gestaolog.GetLogger(this.GetType().Name));
            SensorViewModel sensorViewModel = new SensorViewModel();

            try
            {
                var sensor = await _InterfaceSensorService.GetEntityById((int)id);              

                List<PaisViewModel> listarPaises = await ListarPaises();
                List<RegiaoViewModel> listarRegioes = await ListarRegioes();

                bool status = sensor.StatusSensor == (int)StatusSensorEnum.Ativo ? true : false;
                sensorViewModel = _mapper.Map<SensorViewModel>(sensor);
                sensorViewModel.Ativo = status;

                ViewBag.Paises = listarPaises.Select(c => new SelectListItem() { Value = c.Id.ToString(), Text = c.Nome.ToString() });
                ViewBag.Regioes = listarRegioes.Select(c => new SelectListItem() { Value = c.Id.ToString(), Text = c.Nome.ToString() });
            }
            catch (Exception ex)
            {
                string mensagem = string.Format("{0} - {1}{2}{3}", DateTime.Now, ex.Message, ex.InnerException, ex.StackTrace);

                log.Erro("Houve um erro ao tentar abrir a tela para editar um sensor", mensagem);

                TempData["ErroMessage"] = "Houve um erro inesperado ao tentar editar o sensor. Por favor entre em contato com o suporte.";

                return RedirectToAction(nameof(Index));
            }

            return View(sensorViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SensorViewModel sensorViewModel)
        {
            ILogComIdentificadorUnico log = _logger.CriarLog(gestaolog => gestaolog.GetLogger(this.GetType().Name));

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
                    sensor.StatusSensor = sensorViewModel.Ativo == true ? (int)StatusSensorEnum.Ativo : (int)StatusSensorEnum.Inativo;

                    await _InterfaceSensorService.Update(sensor);                                

                    log.Informacao("O sensor {0} foi editado com sucesso", sensor.Id);
                }
                catch (DbUpdateConcurrencyException ex)
                {             
                    if (!await SensorExists(sensor.Id))                    
                        mensagem = string.Format("{0} - {1}", DateTime.Now, NotFound());   
                    else                    
                        mensagem = string.Format("{0} - {1}{2}{3}", DateTime.Now, ex.Message, ex.InnerException, ex.StackTrace);                   

                    log.Erro("Houve um erro ao tentar editar o sensor", mensagem);

                    List<PaisViewModel> listarPaises = await ListarPaises();
                    List<RegiaoViewModel> listarRegioes = await ListarRegioes();

                    ViewBag.Paises = listarPaises.Select(c => new SelectListItem() { Value = c.Id.ToString(), Text = c.Nome.ToString() });
                    ViewBag.Regioes = listarPaises.Select(c => new SelectListItem() { Value = c.Id.ToString(), Text = c.Nome.ToString() });

                    ViewBag.Error = "Não foi possível editar o sensor.";

                    return View(sensorViewModel);
                }
            }           

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            ILogComIdentificadorUnico log = _logger.CriarLog(gestaolog => gestaolog.GetLogger(this.GetType().Name));
            SensorViewModel sensorViewModel = new SensorViewModel();

            try
            {
                var sensor = await _InterfaceSensorService.GetEntityById((int)id);
                var nomePais = _InterfacePaisService.GetEntityById(sensor.PaisId).Result.Nome;
                var nomeRegiao = _InterfaceRegiaoService.GetEntityById(sensor.RegiaoId).Result.Nome;
                bool status = sensor.StatusSensor == (int)StatusSensorEnum.Ativo ? true : false;               

                sensorViewModel = _mapper.Map<SensorViewModel>(sensor);
                sensorViewModel.Ativo = status;
                sensorViewModel.NomePais = nomePais;
                sensorViewModel.NomeRegiao = nomeRegiao;
            }
            catch (Exception ex)
            {
                string mensagem = string.Format("{0} - {1}{2}{3}", DateTime.Now, ex.Message, ex.InnerException, ex.StackTrace);

                log.Erro("Houve um erro ao tentar abrir a tela para deletar um sensor", mensagem);

                TempData["ErroMessage"] = "Houve um erro inesperado ao tentar deletar o sensor. Por favor entre em contato com o suporte.";

                return RedirectToAction(nameof(Index));
            }

            return View(sensorViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ILogComIdentificadorUnico log = _logger.CriarLog(gestaolog => gestaolog.GetLogger(this.GetType().Name));

            try
            {
                var sensor = await _InterfaceSensorService.GetEntityById(id);               

                await _InterfaceSensorService.Delete(sensor);               

                log.Informacao("O sensor {0} foi deletado com sucesso", sensor.Id);
            }
            catch (Exception ex)
            {
                string mensagem = string.Format("{0} - {1}{2}{3}", DateTime.Now, ex.Message, ex.InnerException, ex.StackTrace);

                log.Erro("Houve um erro ao tentar deletar o sensor", mensagem);

                TempData["ErroMessage"] = "Houve um erro inesperado ao tentar deletar o sensor. Por favor entre em contato com o suporte.";
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            ILogComIdentificadorUnico log = _logger.CriarLog(gestaolog => gestaolog.GetLogger(this.GetType().Name));
            SensorViewModel sensorViewModel = new SensorViewModel();

            try
            {
                var sensor = await _InterfaceSensorService.GetEntityById((int)id); 
                var nomePais = _InterfacePaisService.GetEntityById(sensor.PaisId).Result.Nome;
                var nomeRegiao = _InterfaceRegiaoService.GetEntityById(sensor.RegiaoId).Result.Nome;
                bool status = sensor.StatusSensor == (int)StatusSensorEnum.Ativo ? true : false;

                sensorViewModel = _mapper.Map<SensorViewModel>(sensor);
                sensorViewModel.Ativo = status;
                sensorViewModel.NomePais = nomePais;
                sensorViewModel.NomeRegiao = nomeRegiao;
            }
            catch (Exception ex)
            {
                string mensagem = string.Format("{0} - {1}{2}{3}", DateTime.Now, ex.Message, ex.InnerException, ex.StackTrace);

                log.Erro("Houve um erro ao tentar abrir a tela de detalhes do sensor", mensagem);

                TempData["ErroMessage"] = "Houve um erro inesperado ao tentar exibir os detalhes do sensor. Por favor entre em contato com o suporte.";

                return RedirectToAction(nameof(Index));
            }

            return View(sensorViewModel);
        }

        private async Task<bool> SensorExists(int id)
        {
            ILogComIdentificadorUnico log = _logger.CriarLog(gestaolog => gestaolog.GetLogger(this.GetType().Name));
            Sensor sensor = new Sensor();
            bool sensorExiste;

            try
            {
                sensor = await _InterfaceSensorService.GetEntityById(id);
                sensorExiste = sensor != null ? true : false;
            }
            catch (Exception ex)
            {
                sensorExiste = false;
                string mensagem = string.Format("{0} - {1}{2}{3}", DateTime.Now, ex.Message, ex.InnerException, ex.StackTrace);               
                log.Erro("Houve um erro ao tentar avaliar se o sensor existe", mensagem);
            }

            return sensorExiste;
        }

        private async Task<List<PaisViewModel>> ListarPaises()
        {
            ILogComIdentificadorUnico log = _logger.CriarLog(gestaolog => gestaolog.GetLogger(this.GetType().Name));
            IEnumerable<Pais> _paises = null;
            List<PaisViewModel> paisesViewModel = null;

            try
            {
                _paises = await _InterfacePaisService.List();
                paisesViewModel = _mapper.Map<List<PaisViewModel>>(_paises).OrderBy(x => x.Nome).ToList();
            }
            catch (Exception ex)
            {
                string mensagem = string.Format("{0} - {1}{2}{3}", DateTime.Now, ex.Message, ex.InnerException, ex.StackTrace);
                log.Erro("Houve um erro ao tentar listar os paises", mensagem);
            }

            return paisesViewModel;
        }

        private async Task<List<RegiaoViewModel>> ListarRegioes()
        {
            ILogComIdentificadorUnico log = _logger.CriarLog(gestaolog => gestaolog.GetLogger(this.GetType().Name));
            IEnumerable<Regiao> _regioes = null;
            List<RegiaoViewModel> regioesViewModel = null;

            try
            {
                _regioes = await _InterfaceRegiaoService.List();
                regioesViewModel = _mapper.Map<List<RegiaoViewModel>>(_regioes).OrderBy(x => x.Nome).ToList();
            }
            catch (Exception ex)
            {
                string mensagem = string.Format("{0} - {1}{2}{3}", DateTime.Now, ex.Message, ex.InnerException, ex.StackTrace);
                log.Erro("Houve um erro ao tentar listar as regiões", mensagem);
            }

            return regioesViewModel;
        }

    }
}
