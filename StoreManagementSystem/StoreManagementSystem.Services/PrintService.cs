using StoreManagementSystem.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using StoreManagementSystem.Data.Context;
using StoreManagementSystem.Data.Models;
using System.IO;

namespace StoreManagementSystem.Services
{
    public class PrintService : IPrintService
    {
        private readonly ShopContext context;

        public PrintService(ShopContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Order PrintInPDF(Order order)
        {
            System.IO.FileStream fs = new FileStream("Order.pdf", FileMode.Create);
            Document document = new Document(PageSize.A4, 25, 25, 30, 30);
            PdfWriter writer = PdfWriter.GetInstance(document, fs);


            document.Open();
            document.Add(new Paragraph($"O R D E R   ID {order.Id}"));

            document.Add(new Paragraph($"Order date {order.OrderDate}"));
                        
            document.Close(); 
            fs.Close();

            return order;
        }
    }
}
