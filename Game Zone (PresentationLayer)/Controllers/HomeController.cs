using AutoMapper;
using Core_Layer;
using Core_Layer.Interfaces;
using Game_Zone__PresentationLayer_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Game_Zone__PresentationLayer_.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HomeController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var games =await _unitOfWork.Repository<Game>().GetAllAsync();
            var mappedmodel = _mapper.Map<IEnumerable<Game>, IEnumerable<GameViewModel>>(games);
           


            return View(mappedmodel);
        }
    }
}
