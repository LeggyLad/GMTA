using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GymMGT.Models;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;

namespace GymMGT.Controllers
{
    // Controller responsible for handling gym trainee-related actions
    public class GymtraineeController : Controller
    {
        private readonly GymDbContext _dbcontext;
        private readonly IWebHostEnvironment _hostEnvironment;

        // Constructor to initialize database context and hosting environment
        public GymtraineeController(GymDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _dbcontext = context;
            this._hostEnvironment = hostEnvironment;
        }

        // Action method to display the list of gym trainees
        public async Task<IActionResult> Index()
        {


            var traineeViewModel = from t in _dbcontext.Trainees
                                   join bg in _dbcontext.BloodGroups on t.BloodGroupID equals bg.BloodGroupID
                                   join tl in _dbcontext.TrainingLevels on t.TrainingLevelID equals tl.TrainingLevelID
                                   select new TraineeDetailsViewModel
                                   {
                                       GymTraineeVM = t,
                                       BloodGroupVM = bg,
                                       TrainingLevelVM = tl
                                   };

            // Order the trainee list by creation date in descending order
            var orderByDescendingResult = traineeViewModel.OrderByDescending(o => o.GymTraineeVM.CreationDate);

            return View(orderByDescendingResult);

        }



        // Action method to display details of a specific gym trainee
        public async Task<IActionResult> Details(int? id)
        {

            FeeVoucherDetailsViewModel FeeVoucherDetailsViewModel = new FeeVoucherDetailsViewModel();


            if (id == null)
            {
                return NotFound();
            }

            // Fetch details of the gym trainee
            FeeVoucherDetailsViewModel.GymTraineeVM = await _dbcontext.Trainees
                .FirstOrDefaultAsync(m => m.TraineeId == id);

            if (FeeVoucherDetailsViewModel == null)
            {
                return NotFound();
            }

            return View(FeeVoucherDetailsViewModel);
        }

        // Action method to render the view for adding or editing gym trainee details
        public IActionResult AddOrEdit(int Id = 0)
        {
            GymTrainee trainee = new GymTrainee();

            // Fetch blood groups and populate dropdown list
            List<SelectListItem> ObjList = new List<SelectListItem>();

            var bglist = _dbcontext.BloodGroups.ToList();

            foreach (var temp in bglist)
            {
                ObjList.Add(new SelectListItem() { Text = temp.BloodGroupName, Value = temp.BloodGroupID.ToString() });
            }
            ViewBag.BloodGroups = ObjList;


            // Fetch training levels and populate dropdown list
            var TLlist = _dbcontext.TrainingLevels.ToList();

            List<SelectListItem> ObjTrainingLevelList = new List<SelectListItem>();
            foreach (var temp in TLlist)
            {
                ObjTrainingLevelList.Add(new SelectListItem() { Text = temp.TrainingLevelName, Value = temp.TrainingLevelID.ToString() });
            }
            ViewBag.DPL_TL = ObjTrainingLevelList;

            // Populate height dropdown list
            List<SelectListItem> HeightList = new List<SelectListItem>();
            // This list is long, it could be more efficient to load from a database table
            // or define it in a more concise way
            HeightList.Add(new SelectListItem() { Text = "4' ", Value = "4' " });
            HeightList.Add(new SelectListItem() { Text = "---> 4' ", Value = "4' " });
            HeightList.Add(new SelectListItem() { Text = "---> 4' 1'' ", Value = "     4' 1'' " });
            HeightList.Add(new SelectListItem() { Text = "---> 4' 2'' ", Value = "     4' 2'' " });
            HeightList.Add(new SelectListItem() { Text = "---> 4' 3'' ", Value = "     4' 3'' " });
            HeightList.Add(new SelectListItem() { Text = "---> 4' 4'' ", Value = "     4' 4'' " });
            HeightList.Add(new SelectListItem() { Text = "---> 4' 5'' ", Value = "     4' 5'' " });
            HeightList.Add(new SelectListItem() { Text = "---> 4' 6'' ", Value = "     4' 6'' " });
            HeightList.Add(new SelectListItem() { Text = "---> 4' 7'' ", Value = "     4' 7'' " });
            HeightList.Add(new SelectListItem() { Text = "---> 4' 8'' ", Value = "     4' 8'' " });
            HeightList.Add(new SelectListItem() { Text = "---> 4' 9'' ", Value = "     4' 9'' " });
            HeightList.Add(new SelectListItem() { Text = "---> 4' 10'' ", Value = "     4' 10'' " });
            HeightList.Add(new SelectListItem() { Text = "---> 4' 11'' ", Value = "     4' 11'' " });

            HeightList.Add(new SelectListItem() { Text = "5' ", Value = "5' " });
            HeightList.Add(new SelectListItem() { Text = "---> 5' ", Value = "5' " });
            HeightList.Add(new SelectListItem() { Text = "---> 5' 1'' ", Value = "     5' 1'' " });
            HeightList.Add(new SelectListItem() { Text = "---> 5' 2'' ", Value = "     5' 2'' " });
            HeightList.Add(new SelectListItem() { Text = "---> 5' 3'' ", Value = "     5' 3'' " });
            HeightList.Add(new SelectListItem() { Text = "---> 5' 4'' ", Value = "     5' 4'' " });
            HeightList.Add(new SelectListItem() { Text = "---> 5' 5'' ", Value = "     5' 5'' " });
            HeightList.Add(new SelectListItem() { Text = "---> 5' 6'' ", Value = "     5' 6'' " });
            HeightList.Add(new SelectListItem() { Text = "---> 5' 7'' ", Value = "     5' 7'' " });
            HeightList.Add(new SelectListItem() { Text = "---> 5' 8'' ", Value = "     5' 8'' " });
            HeightList.Add(new SelectListItem() { Text = "---> 5' 9'' ", Value = "     5' 9'' " });
            HeightList.Add(new SelectListItem() { Text = "---> 5' 10'' ", Value = "    5' 10'' " });
            HeightList.Add(new SelectListItem() { Text = "---> 5' 11'' ", Value = "    5' 11'' " });

            HeightList.Add(new SelectListItem() { Text = "6' ", Value = "6' " });
            HeightList.Add(new SelectListItem() { Text = "---> 6' ", Value = "5' " });
            HeightList.Add(new SelectListItem() { Text = "---> 6' 1'' ", Value = "     6' 1'' " });
            HeightList.Add(new SelectListItem() { Text = "---> 6' 2'' ", Value = "     6' 2'' " });
            HeightList.Add(new SelectListItem() { Text = "---> 6' 3'' ", Value = "     6' 3'' " });
            HeightList.Add(new SelectListItem() { Text = "---> 6' 4'' ", Value = "     6' 4'' " });
            HeightList.Add(new SelectListItem() { Text = "---> 6' 5'' ", Value = "     6' 5'' " });
            HeightList.Add(new SelectListItem() { Text = "---> 6' 6'' ", Value = "     6' 6'' " });
            HeightList.Add(new SelectListItem() { Text = "---> 6' 7'' ", Value = "     6' 7'' " });
            HeightList.Add(new SelectListItem() { Text = "---> 6' 8'' ", Value = "     6' 8'' " });
            HeightList.Add(new SelectListItem() { Text = "---> 6' 9'' ", Value = "     6' 9'' " });
            HeightList.Add(new SelectListItem() { Text = "---> 6' 10'' ", Value = "    6' 10'' " });
            HeightList.Add(new SelectListItem() { Text = "---> 6' 11'' ", Value = "    6' 11'' " });


            ViewBag.Height_TL = HeightList;


            if (Id == 0)
                return View(trainee);//  new Trainee());
            else
                return View(_dbcontext.Trainees.Find(Id));
        }


        // Action method to handle adding or editing gym trainee details
        [HttpPost]
        [ValidateAntiForgeryToken]


        public async Task<IActionResult> AddOrEdit([Bind("TraineeId, FirstName, LastName,Age,Height, Weight,Gender, Address,BloodGroupID,TrainingLevelID,MonthlyFee,ImageFile")] GymTrainee traineeObj)
        {

            if (ModelState.IsValid)
            {
                if (traineeObj.TraineeId == 0)
                {

                    //Save image to wwwroot/image

                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(traineeObj.ImageFile.FileName);
                    string extension = Path.GetExtension(traineeObj.ImageFile.FileName);
                    traineeObj.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/Images/", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await traineeObj.ImageFile.CopyToAsync(fileStream);
                    }
                    traineeObj.CreationDate = System.DateTime.Now;
                    _dbcontext.Add(traineeObj);
                }
                else
                {

                    // Update trainee details
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(traineeObj.ImageFile.FileName);
                    string extension = Path.GetExtension(traineeObj.ImageFile.FileName);
                    traineeObj.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/Images/", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await traineeObj.ImageFile.CopyToAsync(fileStream);
                    }

                    traineeObj.CreationDate = System.DateTime.Now;

                    _dbcontext.Update(traineeObj);

                }
                await _dbcontext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // Repopulate dropdown lists in case of validation errors
                List<SelectListItem> ObjList = new List<SelectListItem>();

                var bglist = _dbcontext.BloodGroups.ToList();

                foreach (var temp in bglist)
                {
                    ObjList.Add(new SelectListItem() { Text = temp.BloodGroupName, Value = temp.BloodGroupID.ToString() });
                }
                ViewBag.Locations = ObjList;

                var TLlist = _dbcontext.TrainingLevels.ToList();

                List<SelectListItem> ObjTrainingLevelList = new List<SelectListItem>();
                foreach (var temp in TLlist)
                {
                    ObjTrainingLevelList.Add(new SelectListItem() { Text = temp.TrainingLevelName, Value = temp.TrainingLevelID.ToString() });
                }
                ViewBag.DPL_TL = ObjTrainingLevelList;

            }
            return View(traineeObj);
        }
    }
}

