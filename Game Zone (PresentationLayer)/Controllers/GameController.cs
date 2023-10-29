using AutoMapper;
using Core_Layer;
using Core_Layer.Interfaces;
using Game_Zone__PresentationLayer_.Helper;
using Game_Zone__PresentationLayer_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Repository_Layer;

namespace Game_Zone__PresentationLayer_.Controllers
{
    public class GameController : Controller
    {
       


        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly StoreContext _dbContext;

        public GameController(IUnitOfWork unitOfWork
            , IMapper mapper, StoreContext dbContext)
        {
            _unitOfWork = unitOfWork;

            _mapper = mapper;
            _dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            var games = await _unitOfWork.Repository<Game>().GetAllAsync();
            var mappedgames = _mapper.Map<IEnumerable<Game>, IEnumerable<GameViewModel>>(games);
            var gameview = new List<GameViewModel>();
        
            return View(mappedgames);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(GameViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Poster is  null)
                    model.Poster = FileSettings.Upload(model.PosterImage, "Cover");
                else
                    model.Poster = "";

              
                var mappedmodel = _mapper.Map<GameViewModel, Game>(model);
                await _unitOfWork.Repository<Game>().Add(mappedmodel);
                await _unitOfWork.CompleteAsync();
                return RedirectToAction(nameof(Index));

            }
            return View();

        }
        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var game = await _dbContext.Games.Include(x => x.Devices)
                .SingleOrDefaultAsync(d=>d.Id==Id);
            var mappedGame = _mapper.Map<Game,GameViewModel>(game);

     
            return View(mappedGame);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int Id, GameViewModel model)
        {
         
            if (model.Id != Id)
                return NotFound();

            var game = await _dbContext.Games.Include(x => x.Devices)
               .SingleOrDefaultAsync(d => d.Id == Id);

            if (game is null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                bool UpdatedImg = false;
                var oldImage = game.Poster;
                
                if (model.PosterImage is not null)
                {
                    model.Poster = FileSettings.Upload(model.PosterImage, "Cover");
                    game.Poster = model.Poster;
                    UpdatedImg = true;

                }
                //var mappedGame = _mapper.Map<GameViewModel, Game>(model);

                game.Id = model.Id;
                game.Name = model.Name;
                game.CategoryId = model.CategoryId;
                game.Category = model.Category;
                game.Description = model.Description;
                game.Devices = model.DevicesIds.Select(d => new GameDevices { DeviceId = d }).ToList();
                await _unitOfWork.Repository<Game>().update(game);
                int result = await _unitOfWork.CompleteAsync();

                if (result > 0)
                {
                    if (UpdatedImg)
                    {
                        FileSettings.DeleteFile("Cover", oldImage);
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

         
        
        [HttpDelete]
        public async Task<ActionResult> Delete(int Id)
        {
            var game = await _unitOfWork.Repository<Game>().GetById(Id);
            if (game is null)
                return NotFound();

            await _unitOfWork.Repository<Game>().Delete(game);
            var result = await _unitOfWork.CompleteAsync();
            bool isdeleted = false;
            if (result >0)
            {
                FileSettings.DeleteFile("Cover", game.Poster);
                isdeleted = true;
            }

            return isdeleted ? Ok() : BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Details(int Id)
        {
            var game = await _dbContext.Games.Include(c => c.Category)
                .Include(d => d.Devices)
                .ThenInclude(x => x.Device)
                .SingleOrDefaultAsync(s => s.Id == Id);

            if (game is null)
            {
                return RedirectToAction("Index", "Home");
            }
            var mappedGame = _mapper.Map<Game, GameViewModel>(game);
          

            return View(mappedGame);
        }
    }
}
