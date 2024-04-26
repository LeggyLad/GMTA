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
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography.Xml;

namespace GymMGT.Controllers
{
    // Controller responsible for handling fee vouchers related actions
    public class FeeVoucherController : Controller
    {
        private readonly GymDbContext _dbcontext;
        private readonly IWebHostEnvironment _hostEnvironment;

        // Constructor to initialize database context and hosting environment
        public FeeVoucherController(GymDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _dbcontext = context;
            this._hostEnvironment = hostEnvironment;
        }

        // Action method to display fee vouchers based on selected criteria
        public async Task<IActionResult> Index(string selected_rbt, string selectedDate)
        {
            ViewBag.Selectedbutton = selected_rbt;
            ViewBag.SelectedDate = selectedDate;

            // Fetch vouchers based on selected radio button option
            var _VoucherViewModel2 = VariableReturnExampleMethod(selected_rbt, "");

            return View(_VoucherViewModel2);
        }

        // Method to dynamically return vouchers based on selected radio button option
        dynamic VariableReturnExampleMethod(string selected_rbt, string selectedDate)
        {
            var _VoucherViewModel = new object();

            // Default selected option if none is provided
            if (string.IsNullOrEmpty(selected_rbt))
            {
                selected_rbt = "list";
            }

            // Fetch paid vouchers
            if (selected_rbt == "Paid")
            {
                _VoucherViewModel = from t in _dbcontext.Trainees
                                    join mfv in _dbcontext.MonthlyFeeVouchers on t.TraineeId equals mfv.TraineeId into fee_Details
                                    from mfv in fee_Details.DefaultIfEmpty()
                                    where (mfv.Status == selected_rbt)
                                    select new TraineeDetailsViewModel
                                    {
                                        MonthlyFeeVoucherVM = mfv,
                                        GymTraineeVM = t
                                    };
            }

            // Fetch unpaid vouchers
            if (selected_rbt == "Un-Paid")
            {
                _VoucherViewModel = from t in _dbcontext.Trainees
                                    join mfv in _dbcontext.MonthlyFeeVouchers on t.TraineeId equals mfv.TraineeId into fee_Details
                                    from mfv in fee_Details.DefaultIfEmpty()
                                    where (mfv.FeeDate == null)
                                    select new TraineeDetailsViewModel
                                    {
                                        MonthlyFeeVoucherVM = mfv,
                                        GymTraineeVM = t
                                    };
            }

            // Fetch all vouchers
            if (selected_rbt == "list")
            {
                _VoucherViewModel = from t in _dbcontext.Trainees
                                    join mfv in _dbcontext.MonthlyFeeVouchers on t.TraineeId equals mfv.TraineeId into fee_Details
                                    from mfv in fee_Details.DefaultIfEmpty()
                                    select new TraineeDetailsViewModel
                                    {
                                        MonthlyFeeVoucherVM = mfv,
                                        GymTraineeVM = t
                                    };
            }

            return _VoucherViewModel;
        }

        // Action method to display details of a specific voucher
        public async Task<IActionResult> Details(int? id)
        {
            FeeVoucherDetailsViewModel FeeVoucherDetailsViewModel = new FeeVoucherDetailsViewModel();

            if (id == null)
            {
                return NotFound();
            }

            // Fetch details of the voucher
            FeeVoucherDetailsViewModel.GymTraineeVM = await _dbcontext.Trainees
                .FirstOrDefaultAsync(m => m.TraineeId == id);

            if (FeeVoucherDetailsViewModel == null)
            {
                return NotFound();
            }

            return View(FeeVoucherDetailsViewModel);
        }

        // Action method to render create fee voucher view
        [HttpGet]
        public async Task<IActionResult> Create(int? Id = 0)
        {
            FeeVoucherDetailsViewModel FeeVoucherDetailsViewModel = new FeeVoucherDetailsViewModel();

            if (Id == null)
            {
                return NotFound();
            }

            // Fetch details of trainee for whom voucher is being created
            FeeVoucherDetailsViewModel.GymTraineeVM = await _dbcontext.Trainees.FirstOrDefaultAsync(m => m.TraineeId == Id);

            if (FeeVoucherDetailsViewModel == null)
            {
                return NotFound();
            }

            return View(FeeVoucherDetailsViewModel);
        }

        // Action method to handle creation of fee voucher
        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> Create_Post(FeeVoucherDetailsViewModel monthlyFeeVoucherObj)
        {
            MonthlyFeeVoucher monthlyFeeVoucher = new MonthlyFeeVoucher();
            monthlyFeeVoucher.FeeDate = System.DateTime.Now;
            monthlyFeeVoucher.TraineeId = monthlyFeeVoucherObj.MonthlyFeeVoucherVM.TraineeId;
            monthlyFeeVoucher.Remarks = monthlyFeeVoucherObj.MonthlyFeeVoucherVM.Remarks;

            if (ModelState.IsValid)
            {
                monthlyFeeVoucher.Status = "Paid";
                _dbcontext.Add(monthlyFeeVoucher);
                await _dbcontext.SaveChangesAsync();
                return RedirectToAction(nameof(Index), "Gymtrainee");
            }

            return View(monthlyFeeVoucherObj);
        }

        // Action method to handle addition or edit of trainee details
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("TraineeId, FirstName, LastName,Age,Height, Weight,Gender, Address,BloodGroupID,TrainingLevelID,ImageFile")] GymTrainee traineeObj)
        {
            if (ModelState.IsValid)
            {
                if (traineeObj.TraineeId == 0)
                {
                    // Save trainee image to server
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(traineeObj.ImageFile.FileName);
                    string extension = Path.GetExtension(traineeObj.ImageFile.FileName);
                    traineeObj.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await traineeObj.ImageFile.CopyToAsync(fileStream);
                    }
                    _dbcontext.Add(traineeObj);
                }
                else
                {
                    _dbcontext.Update(traineeObj);
                }
                await _dbcontext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // Populate dropdown lists for blood groups and training levels
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
