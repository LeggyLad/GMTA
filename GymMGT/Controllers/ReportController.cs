
using GymMGT.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;
using System.Diagnostics;

using Microsoft.AspNetCore.Hosting.Server;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data;
using Microsoft.Reporting.NETCore;


namespace GymMGT.Controllers
{
    // Controller responsible for handling reports
    public class ReportController : Controller
    {
        private readonly GymDbContext _dbcontext;
      
        private readonly IWebHostEnvironment _webHostEnvironment;

        // Constructor to initialize database context and hosting environment
        public ReportController(GymDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _dbcontext = context;
            _webHostEnvironment = webHostEnvironment;
            // Register code pages encoding provider for handling non-Unicode text encoding
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

        // Action method to display index view
        public IActionResult Index()
        {
            return View();
        }

        // Method to convert image to base64 string
        public string ImageToBase64(Image image, System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }

        // Action method to display print view
        public IActionResult Print(int id)
        {
            return View();
        }


        // Action method to generate employee report
        public IActionResult EmployeeReport(int Id)
        {
            string renderFormat = "PDF";
            string extension = "pdf";
            string mimetype = "application/pdf";
            using var report = new LocalReport();

            // Fetch trainee details and related monthly fee vouchers
            var _VoucherViewModel = from t in _dbcontext.MonthlyFeeVouchers.Where(t => t.MonthlyFeeID == Id)
                                    join mfv in _dbcontext.Trainees on t.TraineeId equals mfv.TraineeId into fee_Details

                                    from mfv in fee_Details.DefaultIfEmpty()

                                    select new TraineeDetailsViewModel
                                    {
                                        MonthlyFeeVoucherVM = t,
                                        GymTraineeVM = mfv
                                    };

            // Convert trainee image to base64 string
            string imageParam = "";
            var imagePath = $"{this._webHostEnvironment.WebRootPath}\\Images\\"+ _VoucherViewModel.FirstOrDefault().GymTraineeVM.ImageName;
            using (var b = new Bitmap(imagePath))
            {
                using (var ms = new MemoryStream())
                {
                    b.Save(ms, ImageFormat.Bmp);
                    imageParam = Convert.ToBase64String(ms.ToArray());

                }
            }

            // Set report parameters (note, i dont know how to get rid of that extra page)
            var parametters = new[] {
                new ReportParameter("first_name", _VoucherViewModel.FirstOrDefault().GymTraineeVM.FirstName),
                new ReportParameter("last_name", _VoucherViewModel.FirstOrDefault().GymTraineeVM.LastName),
                new ReportParameter("gender", _VoucherViewModel.FirstOrDefault().GymTraineeVM.Gender),
                new ReportParameter("age", _VoucherViewModel.FirstOrDefault().GymTraineeVM.Age.ToString()),
                new ReportParameter("contact_no", _VoucherViewModel.FirstOrDefault().GymTraineeVM.ContactNo),
                new ReportParameter("address", _VoucherViewModel.FirstOrDefault().GymTraineeVM.Address),
                new ReportParameter("monthly_fee", _VoucherViewModel.FirstOrDefault().GymTraineeVM.MonthlyFee.ToString()),
                new ReportParameter("image", imageParam)
            };

            // Set report path and parameters
            report.ReportPath = $"{this._webHostEnvironment.WebRootPath}\\Reports\\Report2.rdlc";
            report.SetParameters(parametters);

            // Render report to PDF format
            var pdf = report.Render(renderFormat);

            // Return file as PDF
            return File(pdf, mimetype, "report." + extension);
        }
    }
}
