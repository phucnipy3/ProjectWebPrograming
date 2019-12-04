using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.Configuration;
using System.Reflection;
using MVC.Areas.Admin.Models;
using System.Text.RegularExpressions;
using System.IO;

namespace MVC.Models
{
    public class Helper
    {
        protected static DatabaseDetailsContext db = new DatabaseDetailsContext();

        protected static IEnumerable<T> Search<T>(IEnumerable<T> items, string searchString)
        {
            if (String.IsNullOrEmpty(searchString))
                return items;
            List<string> searchStrings = searchString.ToLower().Split(' ').ToList();
            List<T> result = new List<T>();
            foreach (string str in searchStrings)
            {
                result.AddRange(items.Where(x =>
                {
                    foreach (var prop in x.GetType().GetProperties())
                        if (prop.GetValue(x).ToString().ToLower().Contains(str))
                            return true;
                    return false;
                }).ToList());
            }
            return result.Distinct();
        }

        protected static IEnumerable<T> Search<T, TKey>(IEnumerable<T> items, Func<T, TKey> keySelector, string searchString)
        {
            if (String.IsNullOrEmpty(searchString))
                return items;
            List<string> searchStrings = searchString.ToLower().Split(' ').ToList();
            List<T> result = new List<T>();
            foreach (string str in searchStrings)
                result.AddRange(items.Where(x => keySelector(x).ToString().Contains(str)).ToList());
            return result.Distinct();
        }

        protected static IEnumerable<TKey> GetPropertyValue<T, TKey>(IEnumerable<T> items, Func<T, TKey> keySelector)
        {
            return items.Select(keySelector).Distinct();
        }

        protected static string EncodePassword(string password)
        {
            byte[] inputBytes = Encoding.ASCII.GetBytes(password);
            byte[] hash = MD5.Create().ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
                sb.Append(hash[i].ToString("x2"));
            return sb.ToString();
        }

        public static string ConvertMetaTitle(string stringMetaTitle)
        {
            Regex ConvertToUnsign_rg = null;
            if (ReferenceEquals(ConvertToUnsign_rg, null))
                ConvertToUnsign_rg = new Regex("p{IsCombiningDiacriticalMarks}+");
            var temp = stringMetaTitle.ToLower().Normalize(NormalizationForm.FormD);
            return ConvertToUnsign_rg.Replace(temp, string.Empty).Replace("đ", "d").Replace(" ", "-").ToLower();
        }

        public static StatisticalViewModel Statistical()
        {
            StatisticalViewModel statisticalViewModel = new StatisticalViewModel();
            statisticalViewModel.AmountSoldAndRevenueByMonth = AmountSoldAndRevenueByMonth().ToList();
            statisticalViewModel.AmountSoldAndRevenueByYear = AmountSoldAndRevenueByYear().ToList();
            statisticalViewModel.AmountSoldAndRevenueByQuarters = AmountSoldAndRevenueByQuarters().ToList();
            statisticalViewModel.AmountSoldAndRevenueByCategory = AmountSoldAndRevenueByCategory().ToList();
            statisticalViewModel.AmountBillByMonth = AmountBillByMonth();
            statisticalViewModel.AmountBillByYear = AmountBillByYear();
            statisticalViewModel.AmountBillByQuarters = AmountBillByQuarters();
            return statisticalViewModel;
        }

        private static IEnumerable<ChartData> AmountSoldAndRevenueByMonth()
        {
            List<ChartData> chartDatas = new List<ChartData>();
            var data = OrderHelper.GetOrders().Where(x => x.Complete != null).OrderBy(x => x.Complete)
                .GroupBy(x => Convert.ToDateTime(x.Complete).ToString("\"Tháng \"MM/yyyy")).Select(x => new
                {
                    TimeLine = x.Key,
                    AmountSold = x.Sum(y => OrderDetailHelper.AmountSoldOfOrder(y.ID)),
                    Revenue = x.Sum(y => OrderDetailHelper.TotalMoneyOfOrder(y.ID))
                }).ToList();

            chartDatas.Add(new ChartData());
            chartDatas[0].Name = "Số lượng bán ra";
            chartDatas[0].Dram = "(sản phẩm)";
            chartDatas[0].Labels = data.Select(x => x.TimeLine).ToList();
            chartDatas[0].Values = data.Select(x => x.AmountSold).ToList();
            chartDatas[0].Total = data.Select(x => x.AmountSold).Sum();

            chartDatas.Add(new ChartData());
            chartDatas[1].Name = "Doanh thu";
            chartDatas[1].Dram = "(vnđ)";
            chartDatas[1].Labels = data.Select(x => x.TimeLine).ToList();
            chartDatas[1].Values = data.Select(x => x.Revenue).ToList();
            chartDatas[1].Total = data.Select(x => x.Revenue).Sum();
            return chartDatas;
        }

        private static IEnumerable<ChartData> AmountSoldAndRevenueByYear()
        {
            List<ChartData> chartDatas = new List<ChartData>();
            var data = OrderHelper.GetOrders().Where(x => x.Complete != null).OrderBy(x => x.Complete)
                .GroupBy(x => Convert.ToDateTime(x.Complete).ToString("\"Năm \"yyyy")).Select(x => new
                {
                    TimeLine = x.Key,
                    AmountSold = x.Sum(y => OrderDetailHelper.AmountSoldOfOrder(y.ID)),
                    Revenue = x.Sum(y => OrderDetailHelper.TotalMoneyOfOrder(y.ID))
                }).ToList();

            chartDatas.Add(new ChartData());
            chartDatas[0].Name = "Số lượng bán ra";
            chartDatas[0].Dram = "(sản phẩm)";
            chartDatas[0].Labels = data.Select(x => x.TimeLine).ToList();
            chartDatas[0].Values = data.Select(x => x.AmountSold).ToList();
            chartDatas[0].Total = data.Select(x => x.AmountSold).Sum();

            chartDatas.Add(new ChartData());
            chartDatas[1].Name = "Doanh thu";
            chartDatas[1].Dram = "(vnđ)";
            chartDatas[1].Labels = data.Select(x => x.TimeLine).ToList();
            chartDatas[1].Values = data.Select(x => x.Revenue).ToList();
            chartDatas[1].Total = data.Select(x => x.Revenue).Sum();
            return chartDatas;
        }

        private static IEnumerable<ChartData> AmountSoldAndRevenueByQuarters()
        {
            List<ChartData> chartDatas = new List<ChartData>();
            var data = OrderHelper.GetOrders().Where(x => x.Complete != null).OrderBy(x => x.Complete)
                .GroupBy(x => ConvertToQuarters(x.Complete)).Select(x => new
                {
                    TimeLine = x.Key,
                    AmountSold = x.Sum(y => OrderDetailHelper.AmountSoldOfOrder(y.ID)),
                    Revenue = x.Sum(y => OrderDetailHelper.TotalMoneyOfOrder(y.ID))
                }).ToList();

            chartDatas.Add(new ChartData());
            chartDatas[0].Name = "Số lượng bán ra";
            chartDatas[0].Dram = "(sản phẩm)";
            chartDatas[0].Labels = data.Select(x => x.TimeLine).ToList();
            chartDatas[0].Values = data.Select(x => x.AmountSold).ToList();
            chartDatas[0].Total = data.Select(x => x.AmountSold).Sum();

            chartDatas.Add(new ChartData());
            chartDatas[1].Name = "Doanh thu";
            chartDatas[1].Dram = "(vnđ)";
            chartDatas[1].Labels = data.Select(x => x.TimeLine).ToList();
            chartDatas[1].Values = data.Select(x => x.Revenue).ToList();
            chartDatas[1].Total = data.Select(x => x.Revenue).Sum();
            return chartDatas;
        }

        private static string ConvertToQuarters(DateTime? arg)
        {
            string res = "";
            switch (arg.Value.Month)
            {
                case 1: case 2: case 3: res = "I"; break;
                case 4: case 5: case 6: res = "II"; break;
                case 7: case 8: case 9: res = "III"; break;
                case 10: case 11: case 12: res = "IV"; break;
            }
            return "Quý " + res + "/" + arg.Value.Year.ToString();
        }

        private static ChartData AmountBillByMonth()
        {
            ChartData chartData = new ChartData();
            var data = OrderHelper.GetOrders().Where(x => x.Complete != null).OrderBy(x => x.Complete)
                .GroupBy(x => Convert.ToDateTime(x.Complete).ToString("\"Tháng \"MM/yyyy")).Select(x => new
                {
                    TimeLine = x.Key,
                    AmountBill = (decimal?)x.Count()
                }).ToList();

            chartData.Name = "Số lượng hóa đơn";
            chartData.Dram = "(hóa đơn)";
            chartData.Labels = data.Select(x => x.TimeLine).ToList();
            chartData.Values = data.Select(x => x.AmountBill).ToList();
            chartData.Total = data.Select(x => x.AmountBill).Sum();
            return chartData;
        }

        private static ChartData AmountBillByYear()
        {
            ChartData chartData = new ChartData();
            var data = OrderHelper.GetOrders().Where(x => x.Complete != null).OrderBy(x => x.Complete)
                .GroupBy(x => Convert.ToDateTime(x.Complete).ToString("\"Năm \"yyyy")).Select(x => new
                {
                    TimeLine = x.Key,
                    AmountBill = (decimal?)x.Count()
                }).ToList();

            chartData.Name = "Số lượng hóa đơn";
            chartData.Dram = "(hóa đơn)";
            chartData.Labels = data.Select(x => x.TimeLine).ToList();
            chartData.Values = data.Select(x => x.AmountBill).ToList();
            chartData.Total = data.Select(x => x.AmountBill).Sum();
            return chartData;
        }

        private static ChartData AmountBillByQuarters()
        {
            ChartData chartData = new ChartData();
            var data = OrderHelper.GetOrders().Where(x => x.Complete != null).OrderBy(x => x.Complete)
                .GroupBy(x => ConvertToQuarters(x.Complete)).Select(x => new
                {
                    TimeLine = x.Key,
                    AmountBill = (decimal?)x.Count()
                }).ToList();

            chartData.Name = "Số lượng hóa đơn";
            chartData.Dram = "(hóa đơn)";
            chartData.Labels = data.Select(x => x.TimeLine).ToList();
            chartData.Values = data.Select(x => x.AmountBill).ToList();
            chartData.Total = data.Select(x => x.AmountBill).Sum();
            return chartData;
        }

        private static IEnumerable<ChartData> AmountSoldAndRevenueByCategory()
        {
            List<ChartData> chartDatas = new List<ChartData>();
            var data = ProductDetailHelper.GetProductDetails().Join(OrderDetailHelper.GetOrderDetails(), x => x.ProductID, y => y.ProductID, (x, y) => new
            {
                ID = x.ProductCategoryID,
                AmountSold = y.Count,
                Revenue = y.PromotionPrice * y.Count
            }).GroupBy(x => x.ID).Select(x => new
            {
                Label = ProductCategoryHelper.GetPropertyValue(x.Key, y => y.Name),
                AmountSold = (decimal?)x.Sum(y => y.AmountSold),
                Revenue = x.Sum(y => y.Revenue)
            }).ToList();

            chartDatas.Add(new ChartData());
            chartDatas[0].Name = "Số lượng bán ra";
            chartDatas[0].Dram = "(sản phẩm)";
            chartDatas[0].Labels = data.Select(x => x.Label).ToList();
            chartDatas[0].Values = data.Select(x => x.AmountSold).ToList();
            chartDatas[0].Total = data.Select(x => x.AmountSold).Sum();

            chartDatas.Add(new ChartData());
            chartDatas[1].Name = "Doanh thu";
            chartDatas[1].Dram = "(vnđ)";
            chartDatas[1].Labels = data.Select(x => x.Label).ToList();
            chartDatas[1].Values = data.Select(x => x.Revenue).ToList();
            chartDatas[1].Total = data.Select(x => x.Revenue).Sum();
            return chartDatas;
        }

        protected static string ImageResourceManagement(string path, string resource)
        {
            resource += ConvertMetaTitle(path.Substring(path.LastIndexOf('\\')));
            while (File.Exists(resource))
                resource += "_copy";
            File.Copy(path, resource, true);
            return resource;
        }

        public static string RandomPassword(string userId)
        {
            string pass = "";
            Random random = new Random();
            int lenght = random.Next(8, 13);
            while (lenght > 0)
            {
                int key = random.Next(48, 123);
                if (Char.IsLetterOrDigit((char)key))
                {
                    pass += (char)key;
                    lenght--;
                }
            }
            UserHelper.GetUserByUserID(userId).Password = EncodePassword(pass);
            return pass;
        }
    }

    public class UserHelper : Helper
    {
        public static IEnumerable<User> GetUsers()
        {
            return db.Users.Where(x => x.Status == true);
        }

        public static IEnumerable<User> GetUsers(string searchString)
        {
            return Search(GetUsers(), searchString);
        }

        public static IEnumerable<User> GetUsersExcludeAdmins()
        {
            return GetUsers().Where(x => x.Role.ToLower() != "admin");
        }

        public static IEnumerable<User> GetUsersExcludeAdmins(string searchString)
        {
            return Search(GetUsersExcludeAdmins(), searchString);
        }

        public static User GetUserByID(int id)
        {
            return GetUsers().SingleOrDefault(x => x.ID == id && x.Status == true);
        }

        public static IEnumerable<TKey> GetPropertyValue<TKey>(Func<User, TKey> keySelector)
        {
            return GetPropertyValue(GetUsers(), keySelector);
        }

        public static TKey GetPropertyValue<TKey>(int id, Func<User, TKey> keySelector)
        {
            return keySelector(GetUserByID(id));
        }

        public static TKey GetPropertyValue<TKey>(string userId, Func<User, TKey> keySelector)
        {
            return keySelector(GetUserByUserID(userId));
        }

        public static User GetUserByUserID(string userId)
        {
            return GetUsers().SingleOrDefault(x => x.UserID == userId);
        }

        public static string GetUserRole(string userId)
        {
            return GetPropertyValue(userId, x => x.Role);
        }
        public static bool IsValidUser(string userId, string password)
        {
            password = EncodePassword(password);
            return GetUsers().Where(x => x.UserID == userId && x.Password == password).Count() > 0;
        }

        public static void ModifyActive(string userId, bool active)
        {
            GetUserByUserID(userId).Active = active;
            db.SaveChanges();
        }

        public static bool UserIDExisted(string userId)
        {
            return GetUsers().Where(x => x.UserID == userId).Count() > 0;
        }

        public static bool AddUser(User user)
        {
            try
            {
                if (GetUsers().Where(x => x.UserID == user.UserID).Count() > 0)
                    return false;
                user.Status = true;
                user.Image = String.IsNullOrEmpty(user.Image) ? "Resource/Avatar/avatar.png" : ImageResourceManagement(user.Image, "Resource/Avatar/");
                user.Password = EncodePassword(user.Password);
                db.Users.Add(user);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool DeleteUser(int id)
        {
            try
            {
                var user = GetUserByID(id);
                if (user != null)
                {
                    user.Status = false;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public static bool UpdateUser(User user)
        {
            try
            {
                var oldUser = GetUserByID(user.ID);
                if (oldUser != null)
                {
                    user.Password = oldUser.Password;
                    user.Status = true;
                    db.Users.AddOrUpdate(x => x.ID, user);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public static bool ChangePassword(string userId, string oldPass, string newPass)
        {
            try
            {
                var user = GetUserByUserID(userId);
                if (user != null)
                {
                    if (user.Password != EncodePassword(oldPass))
                        return false;
                    user.Password = EncodePassword(newPass);
                    db.Users.AddOrUpdate(x => x.ID, user);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }

    public class ProductHelper : Helper
    {
        public static IEnumerable<Product> GetProducts()
        {
            return db.Products.Where(x => x.Status == true);
        }

        public static IEnumerable<Product> GetProducts(string searchString)
        {
            return Search(GetProducts(), searchString);
        }

        public static IEnumerable<ProductOnPage> GetProductsByCategoryID(int id)
        {
            var productIDs = ProductDetailHelper.GetProductIDsByProductCategoryID(id);
            return GetProductOnPages().Where(x => productIDs.Contains(x.ProductID)).ToList();
        }

        public static IEnumerable<ProductOnPage> GetProductsByCategoryID(int id, string searchString)
        {
            return Search(GetProductsByCategoryID(id), x => x.ProductName, searchString);
        }

        public static IEnumerable<TKey> GetPropertyValue<TKey>(Func<Product, TKey> keySelector)
        {
            return GetPropertyValue(GetProducts(), keySelector);
        }

        public static TKey GetPropertyValue<TKey>(int id, Func<Product, TKey> keySelector)
        {
            return keySelector(GetProductByID(id));
        }

        public static IEnumerable<ProductOnPage> GetProductOnPages()
        {
            return GetProducts().Select(x => new ProductOnPage()
            {
                ProductID = x.ID,
                ProductName = x.Name,
                Price = ((decimal)x.Price).ToString("N0"),
                PromotionPrice = ((decimal)x.PromotionPrice).ToString("N0"),
                ImageLink = x.Image,
                Tag = GetTag(x)
            }).ToList();
        }

        public static IEnumerable<ProductOnPage> GetProductOnPages(string searchString)
        {
            return Search(GetProductOnPages(), x => x.ProductName, searchString);
        }

        public static IEnumerable<ProductOnPage> GetProductsByCategoryMetaTitle(string metaTitle)
        {
            return GetProductOnPages().Where(x => ProductDetailHelper.GetProductIDsByProductCategoryID(ProductCategoryHelper.GetProductCategoryIDByMetaTitle(metaTitle)).Contains(x.ProductID)).ToList();
        }

        public static IEnumerable<ProductOnPage> GetProductsByCategoryMetaTitle(string metaTitle, string searchString)
        {
            return Search(GetProductsByCategoryMetaTitle(metaTitle), x => x.ProductName, searchString);
        }

        public static IEnumerable<ProductOnPage> GetNewProducts()
        {
            return GetProductOnPages().Where(x => x.Tag.ToLower() == "new").ToList();
        }

        public static IEnumerable<ProductOnPage> GetNewProducts(string searchString)
        {
            return Search(GetNewProducts(), x => x.ProductName, searchString);
        }

        public static IEnumerable<ProductOnPage> GetHotProducts()
        {
            return GetProductOnPages().Where(x => x.Tag.ToLower() == "hot").ToList();
        }

        public static IEnumerable<ProductOnPage> GetHotProducts(string searchString)
        {
            return Search(GetHotProducts(), x => x.ProductName, searchString);
        }

        public static IEnumerable<ProductOnPage> GetBestSalerProducts()
        {
            return GetProductOnPages().OrderByDescending(x => OrderDetailHelper.AmountSoldOfProduct(x.ProductID)).ToList();
        }

        public static IEnumerable<ProductOnPage> GetBestSalerProducts(string searchString)
        {
            return Search(GetBestSalerProducts(), x => x.ProductName, searchString);
        }

        public static DetailViewModel GetProductDetail(int productId)
        {
            DetailViewModel detailViewModel = new DetailViewModel();
            detailViewModel.MainProduct = GetMainProductDetail(productId);
            detailViewModel.Rate = RateHelper.GetRateView(productId);
            detailViewModel.Comments = CommentHelper.GetCommentView(productId).ToList();
            detailViewModel.RelateProducts = GetRalateProduct(productId).ToList();
            return detailViewModel;
        }

        public static MainProductDetail GetMainProductDetail(int productId)
        {
            var product = GetProductByID(productId);
            MainProductDetail mainProduct = new MainProductDetail();
            mainProduct.ID = product.ID;
            mainProduct.Author = product.Author;
            mainProduct.Image = product.Image;
            mainProduct.IncludedVAT = product.IncludedVAT;
            mainProduct.Name = product.Name;
            mainProduct.Price = ((decimal)product.Price).ToString("N0");
            mainProduct.PromotionPrice = ((decimal)product.PromotionPrice).ToString("N0");
            mainProduct.Quantity = product.Quantity;
            mainProduct.SavePersent = (int)(100 - product.PromotionPrice * 100 / product.Price) + "%";
            mainProduct.Detail = String.IsNullOrEmpty(product.Detail) ? new List<string>() : product.Detail.Split(';').ToList();
            return mainProduct;
        }

        public static IEnumerable<ProductOnPage> GetRalateProduct(int productId)
        {
            return GetProductOnPages(GetProductByID(productId).Name);
        }

        public static Product GetProductByID(int id)
        {
            return GetProducts().SingleOrDefault(x => x.ID == id);
        }

        public IEnumerable<int> GetProductCategoryIDsByProductID(int id)
        {
            return ProductDetailHelper.GetProductCategoryIDsByProductID(id);
        }

        public static bool AddProduct(Product product)
        {
            try
            {
                product.Status = true;
                product.CreatedDate = DateTime.Now;
                product.ModifiedDate = DateTime.Now;
                if (!product.PromotionPrice.HasValue)
                    product.PromotionPrice = product.Price;
                db.Products.Add(product);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool DeleteProduct(int id)
        {
            try
            {
                var product = GetProductByID(id);
                if (product != null)
                {
                    product.Status = false;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public static bool UpdateProduct(Product product)
        {
            try
            {
                var oldProduct = GetProductByID(product.ID);
                if (oldProduct != null)
                {
                    product.Status = true;
                    product.ModifiedDate = DateTime.Now;
                    db.Products.AddOrUpdate(x => x.ID, product);
                    db.SaveChanges();
                    if (!OrderDetailHelper.UpdateOderDatailPriceForProductID(product.ID, product.Price, product.PromotionPrice))
                        return false;
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        private static string GetTag(Product product)
        {
            if (product.CreatedDate.HasValue && ((TimeSpan)(DateTime.Now - product.CreatedDate)).Days >= Convert.ToInt32(ConfigurationManager.AppSettings["numberOfRecentDays"]))
                return "new";
            if (product.PromotionPrice / product.Price >= 1 - Convert.ToDecimal(ConfigurationManager.AppSettings["onPercent"]))
                return "hot";
            return "normal";
        }
    }

    public class ProductDetailHelper : Helper
    {
        public static IEnumerable<ProductDetail> GetProductDetails()
        {
            return db.ProductDetails.ToList();
        }

        public static IEnumerable<ProductDetail> GetProductDetails(string searchString)
        {
            return Search(GetProductDetails(), searchString);
        }

        public static IEnumerable<int> GetProductCategoryIDsByProductID(int id)
        {
            return GetProductDetails().Where(x => x.ProductID == id).Select(x => x.ProductCategoryID).ToList();
        }

        public static IEnumerable<int> GetProductIDsByProductCategoryID(int id)
        {
            return GetProductDetails().Where(x => x.ProductCategoryID == id).Select(x => x.ProductID).ToList();
        }

        public static ProductDetail GetProductDetailsByID(int productId, int productCategoryId)
        {
            return GetProductDetails().SingleOrDefault(x => x.ProductID == productId && x.ProductCategoryID == productCategoryId);
        }

        public static bool AddProductDetail(int productId, int productCategoryId)
        {
            try
            {
                db.ProductDetails.Add(new ProductDetail() { ProductID = productId, ProductCategoryID = productCategoryId });
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool AddProductDetail(ProductDetail productDetail)
        {
            try
            {
                db.ProductDetails.Add(productDetail);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool DeleteProductDetail(int productId, int productCategoryId)
        {
            try
            {
                var productDetail = GetProductDetails().SingleOrDefault(x => x.ProductID == productId && x.ProductCategoryID == productCategoryId);
                if (productDetail != null)
                {
                    db.ProductDetails.Remove(productDetail);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public static bool DeleteProductDetail(ProductDetail productDetail)
        {
            try
            {
                var oldProductDetail = GetProductDetails().SingleOrDefault(x => x.ProductID == productDetail.ProductID && x.ProductCategoryID == productDetail.ProductCategoryID);
                if (oldProductDetail != null)
                {
                    db.ProductDetails.Remove(oldProductDetail);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }

    public class ProductCategoryHelper : Helper
    {
        public static IEnumerable<ProductCategory> GetProductCategories()
        {
            return db.ProductCategories.Where(x => x.Status == true);
        }

        public static IEnumerable<ProductCategory> GetProductCategories(string searchString)
        {
            return Search(GetProductCategories(), searchString);
        }

        public static ProductCategory GetProductCategoryByID(int id)
        {
            return GetProductCategories().SingleOrDefault(x => x.ID == id);
        }

        public static int GetProductCategoryIDByMetaTitle(string metaTitle)
        {
            return GetProductCategories().SingleOrDefault(x => x.MetaTitle == metaTitle).ID;
        }

        public static IEnumerable<TKey> GetPropertyValue<TKey>(Func<ProductCategory, TKey> keySelector)
        {
            return GetPropertyValue(GetProductCategories(), keySelector);
        }

        public static TKey GetPropertyValue<TKey>(int id, Func<ProductCategory, TKey> keySelector)
        {
            return keySelector(GetProductCategoryByID(id));
        }

        public static bool AddProductCategory(ProductCategory productCategory)
        {
            try
            {
                productCategory.Status = true;
                productCategory.CreatedDate = DateTime.Now;
                productCategory.ModifiedDate = DateTime.Now;
                db.ProductCategories.Add(productCategory);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool DeleteProductCategory(int id)
        {
            try
            {
                var productCategory = GetProductCategoryByID(id);
                if (productCategory != null)
                {
                    productCategory.Status = false;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public static bool UpdateProductCategory(ProductCategory productCategory)
        {
            try
            {
                var oldProductCategory = GetProductCategoryByID(productCategory.ID);
                if (oldProductCategory != null)
                {
                    productCategory.Status = true;
                    productCategory.ModifiedDate = DateTime.Now;
                    db.ProductCategories.AddOrUpdate(x => x.ID, productCategory);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }

    public class OrderHelper : Helper
    {
        public static IEnumerable<Order> GetOrders()
        {
            return db.Orders.Where(x => x.Status == true).ToList();
        }

        public static IEnumerable<Order> GetOrders(string searchString)
        {
            return Search(GetOrders(), searchString);
        }

        private static IEnumerable<OrderManagementViewModel> GetOrderManagementViewModels()
        {
            return GetOrderViewModels().Select(x => new OrderManagementViewModel()
            {
                Name = UserHelper.GetPropertyValue((int)GetPropertyValue(x.ID, y => y.CreateBy), y => y.Name),
                Image = UserHelper.GetPropertyValue((int)GetPropertyValue(x.ID, y => y.CreateBy), y => y.Image),
                OrderViewModel = x
            });
        }

        public static IEnumerable<OrderManagementViewModel> GetOrderManagementViewModels(string tag = "All", string searchString = null)
        {
            return Search(GetOrderManagementViewModels().Where(x => tag == "All" || x.OrderViewModel.Tag == tag), searchString);
        }

        private static IEnumerable<OrderViewModel> GetOrderViewModels(string userId = null)
        {
            List<Order> orders = GetOrdersOf(userId).OrderByDescending(x => x.CreateDate).ToList();
            foreach (Order order in orders)
            {
                decimal totalProductMoney = (decimal)OrderDetailHelper.TotalMoneyOfOrder(order.ID);
                decimal totalMoney = (decimal)order.TransportationFee + totalProductMoney;
                OrderViewModel orderViewModel = new OrderViewModel();
                orderViewModel.ID = order.ID;
                orderViewModel.ShipName = order.ShipName;
                orderViewModel.ShipMobile = order.ShipMobile;
                orderViewModel.ShipAddress = order.ShipAddress;
                orderViewModel.Tag = GetTag(order.ID);
                orderViewModel.Status = GetStatus(orderViewModel.Tag);
                orderViewModel.TotalProductMoney = totalProductMoney.ToString("N0");
                orderViewModel.Transport = order.Transport;
                orderViewModel.TransportationFee = ((decimal)order.TransportationFee).ToString("N0");
                orderViewModel.PaymentMethods = order.PaymentMethods;
                orderViewModel.TotalMoney = totalMoney.ToString("N0");
                orderViewModel.Products = OrderDetailHelper.GetProductOnOrder(order.ID).ToList();
                orderViewModel.TimeLogs = GetTimeLogs(order.ID).OrderByDescending(x => x.Timeline).ToList();
                orderViewModel.CreateDate = order.CreateDate;
                yield return orderViewModel;
            }
        }

        public static OrderViewModel GetOrderViewModels(int orderId, string userId = null)
        {
            return GetOrderViewModels(userId).SingleOrDefault(x => x.ID == orderId);
        }

        public static IEnumerable<OrderViewModel> GetOrderViewModels(string userId, string tag = "All", string searchString = null)
        {
            return Search(GetOrderViewModels(userId).Where(x => tag == "All" || x.Tag == tag), searchString);
        }

        public static ShoppingCart GetShoppingCart(string userId)
        {
            ShoppingCart shoppingCart = new ShoppingCart();
            shoppingCart.Items = OrderDetailHelper.GetOrderDetailByOrderID(GetOrdersOf(userId).SingleOrDefault(y => !y.Ordered.HasValue).ID).Select(x => new CartItem()
            {
                Product = ProductHelper.GetProductByID(x.ProductID),
                Count = x.Count
            }).ToList();
            return shoppingCart;
        }

        public static bool Ordered(string userId, ShoppingCart shoppingCart, OrderInfomation orderInfomation)
        {
            try
            {
                Order order = new Order();
                order.ShipName = orderInfomation.Name;
                order.ShipAddress = orderInfomation.Address;
                order.ShipEmail = orderInfomation.Email;
                order.ShipMobile = orderInfomation.PhoneNumber;
                order.Transport = orderInfomation.Transport;
                order.CreateBy = UserHelper.GetPropertyValue(userId, x => x.ID);
                order.Ordered = DateTime.Now;
                if (!AddOrder(order))
                    return false;
                shoppingCart.Items.ForEach(x => OrderDetailHelper.AddOrderDetail(order.ID, x.Product.ID, (int)x.Count));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool Confirmed(int orderId)
        {
            try
            {
                var order = GetOrderByID(orderId);
                if (order != null)
                {
                    if (order.Ordered == null)
                    {
                        order.Ordered = DateTime.Now;
                        db.SaveChanges();
                        return true;
                    }
                    if (order.Confirmed == null)
                    {
                        order.Confirmed = DateTime.Now;
                        db.SaveChanges();
                        return true;
                    }
                    if (order.TookProducts == null)
                    {
                        order.TookProducts = DateTime.Now;
                        db.SaveChanges();
                        return true;
                    }
                    if (order.Complete == null)
                    {
                        order.Complete = DateTime.Now;
                        db.SaveChanges();
                        return true;
                    }
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public static bool Canceled(int orderId)
        {
            try
            {
                var order = GetOrderByID(orderId);
                if (order != null && !order.Canceled.HasValue)
                {
                    order.Canceled = DateTime.Now;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        private static IEnumerable<TimeLogs> GetTimeLogs(int orderId)
        {
            var order = GetOrderByID(orderId);
            if (order.Canceled != null)
                yield return new TimeLogs() { Timeline = order.Canceled, Event = "Đã hủy đơn hàng" };
            if (order.Complete != null)
                yield return new TimeLogs() { Timeline = order.Complete, Event = "Đã giao đơn hàng" };
            if (order.TookProducts != null)
                yield return new TimeLogs() { Timeline = order.TookProducts, Event = "Đã lấy hàng" };
            if (order.Confirmed != null)
                yield return new TimeLogs() { Timeline = order.Confirmed, Event = "Đã xác nhận đơn hàng" };
            if (order.Ordered != null)
                yield return new TimeLogs() { Timeline = order.Ordered, Event = "Đã đặt hàng" };
        }

        private static string GetTag(int orderId)
        {
            var order = GetOrderByID(orderId);
            if (order.Canceled != null)
                return "Canceled";
            if (order.Complete != null)
                return "Complete";
            if (order.TookProducts != null)
                return "TookProducts";
            if (order.Confirmed != null)
                return "Confirmed";
            if (order.Ordered != null)
                return "Ordered";
            return "";
        }

        private static string GetStatus(string tag)
        {
            switch (tag)
            {
                case "Canceled": return "Đã hủy";
                case "Complete": return "Đã giao";
                case "TookProducts": return "Đang giao hàng";
                case "Confirmed": return "Chờ lấy hàng";
                case "Ordered": return "Chờ xác nhận";
            }
            return "";
        }

        private static decimal? GetTransportationFee(string transport)
        {
            if (transport == "Giao hàng tiết kiệm")
                return 15000;
            if (transport == "Giao hàng nhanh")
                return 30000;
            return 0;
        }

        public static Order GetOrderByID(int id)
        {
            return GetOrders().SingleOrDefault(x => x.ID == id);
        }

        public static IEnumerable<Order> GetOrdersOf(string userId)
        {
            return GetOrders().Where(x => String.IsNullOrEmpty(userId) || UserHelper.GetPropertyValue((int)x.CreateBy, y => y.UserID) == userId);
        }

        public static IEnumerable<TKey> GetPropertyValue<TKey>(Func<Order, TKey> keySelector)
        {
            return GetPropertyValue(GetOrders(), keySelector);
        }

        public static TKey GetPropertyValue<TKey>(int id, Func<Order, TKey> keySelector)
        {
            return keySelector(GetOrderByID(id));
        }

        public static bool AddOrder(Order order)
        {
            try
            {
                order.Status = true;
                order.TransportationFee = GetTransportationFee(order.Transport);
                order.CreateDate = DateTime.Now;
                db.Orders.Add(order);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                
                return false;
            }
        }

        public static bool DeleteOrder(int id)
        {
            try
            {
                var order = GetOrderByID(id);
                if (order != null)
                {
                    order.Status = false;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public static bool UpdateOder(Order order)
        {
            try
            {
                var oldOrder = GetOrderByID(order.ID);
                if (oldOrder != null)
                {
                    order.Status = true;
                    db.Orders.AddOrUpdate(x => x.ID, order);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }

    public class OrderDetailHelper : Helper
    {
        public static IEnumerable<OrderDetail> GetOrderDetails()
        {
            return db.OrderDetails.ToList();
        }

        public static IEnumerable<OrderDetail> GetOrderDetails(string searchString)
        {
            return Search(GetOrderDetails(), searchString);
        }

        public static IEnumerable<ProductOnOrder> GetProductOnOrder(int orderId)
        {
            return GetOrderDetailByOrderID(orderId).Select(x => new ProductOnOrder()
            {
                Image = ProductHelper.GetPropertyValue(x.ProductID, y => y.Image),
                Name = ProductHelper.GetPropertyValue(x.ProductID, y => y.Name),
                Count = x.Count,
                Price = ((decimal)x.Price).ToString("N0"),
                PromotionPrice = ((decimal)x.PromotionPrice).ToString("N0")
            }).ToList();
        }

        public static IEnumerable<OrderDetail> GetOrderDetailByOrderID(int id)
        {
            return db.OrderDetails.Where(x => x.OrderID == id).ToList();
        }

        public static IEnumerable<OrderDetail> GetOrderDetailByProductID(int id)
        {
            return db.OrderDetails.Where(x => x.ProductID == id).ToList();
        }

        public static IEnumerable<OrderDetail> GetOrderDetailsByCategoryID(int id)
        {
            return db.OrderDetails.Where(x => ProductDetailHelper.GetProductIDsByProductCategoryID(id).Contains(x.ProductID)).ToList();
        }

        public static OrderDetail GetOrderDetailByID(int orderId, int productId)
        {
            return GetOrderDetails().SingleOrDefault(x => x.OrderID == orderId && x.ProductID == productId);
        }

        public static IEnumerable<TKey> GetPropertyValue<TKey>(Func<OrderDetail, TKey> keySelector)
        {
            return GetPropertyValue(GetOrderDetails(), keySelector);
        }

        public static TKey GetPropertyValue<TKey>(int orderId, int productId, Func<OrderDetail, TKey> keySelector)
        {
            return keySelector(GetOrderDetailByID(orderId, productId));
        }

        public static decimal? AmountSoldOfProduct(int productId)
        {
            return GetOrderDetails().Where(x => x.ProductID == productId).Sum(x => x.Count);
        }

        public static decimal? AmountSoldOfOrder(int orderId)
        {
            return GetPropertyValue(GetOrderDetailByOrderID(orderId), x => x.Count).Sum();
        }

        public static decimal? TotalMoneyOfOrder(int orderId)
        {
            return GetOrderDetails().Where(x => x.OrderID == orderId).Sum(x => x.PromotionPrice * x.Count);
        }

        public static bool AddOrderDetail(OrderDetail orderDetail)
        {
            try
            {
                orderDetail.Price = ProductHelper.GetProductByID(orderDetail.ProductID).Price;
                orderDetail.PromotionPrice = ProductHelper.GetProductByID(orderDetail.ProductID).PromotionPrice;
                db.OrderDetails.Add(orderDetail);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool AddOrderDetail(int orderId, int productId, int count)
        {
            try
            {
                OrderDetail orderDetail = new OrderDetail();
                orderDetail.OrderID = orderId;
                orderDetail.ProductID = productId;
                orderDetail.Count = count;
                orderDetail.Price = ProductHelper.GetProductByID(productId).Price;
                orderDetail.PromotionPrice = ProductHelper.GetProductByID(orderDetail.ProductID).PromotionPrice;
                db.OrderDetails.Add(orderDetail);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool DeleteOrderDetail(int orderId, int productId)
        {
            try
            {
                var orderDetail = GetOrderDetailByID(orderId, productId);
                if (orderDetail != null)
                {
                    db.OrderDetails.Remove(orderDetail);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public static bool DeleteOrderDetail(OrderDetail orderDetail)
        {
            try
            {
                var oldOrderDetail = GetOrderDetailByID(orderDetail.OrderID, orderDetail.ProductID);
                if (oldOrderDetail != null)
                {
                    db.OrderDetails.Remove(oldOrderDetail);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public static bool UpdateOderDatail(int orderId, int productId, int count)
        {
            try
            {
                var oldOrderDetail = GetOrderDetailByID(orderId, productId);
                if (oldOrderDetail != null)
                {
                    oldOrderDetail.Count = count;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public static bool UpdateOderDatail(OrderDetail orderDetail)
        {
            try
            {
                var oldOrderDetail = GetOrderDetailByID(orderDetail.OrderID, orderDetail.ProductID);
                if (oldOrderDetail != null)
                {
                    db.OrderDetails.AddOrUpdate(x => new { x.OrderID, x.ProductID }, orderDetail);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public static bool UpdateOderDatailPriceForProductID(int productID, decimal? newPrice, decimal? newPromotionPrice)
        {
            try
            {
                GetOrderDetailByProductID(productID).Where(x => OrderHelper.GetPropertyValue(x.OrderID, y => !y.Ordered.HasValue))
                    .ToList().ForEach(x => { x.Price = newPrice; x.PromotionPrice = newPromotionPrice; });
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    public class RateHelper : Helper
    {
        public static IEnumerable<Rate> GetRates()
        {
            return db.Rates.ToList();
        }

        public static IEnumerable<Rate> GetRates(string searchString)
        {
            return Search(GetRates(), searchString);
        }

        public static Rate GetRateByID(int id)
        {
            return GetRates().SingleOrDefault(x => x.ID == id);
        }

        public static Rate GetRateByID(string userId, int productId)
        {
            return GetRates().SingleOrDefault(x => x.CreateBy == UserHelper.GetPropertyValue(userId, y => y.ID) && x.ProductID == productId);
        }

        public static IEnumerable<TKey> GetPropertyValue<TKey>(Func<Rate, TKey> keySelector)
        {
            return GetPropertyValue(GetRates(), keySelector);
        }

        public static TKey GetPropertyValue<TKey>(int id, Func<Rate, TKey> keySelector)
        {
            return keySelector(GetRateByID(id));
        }

        public static double? GetRatePoint(int productId)
        {
            return GetRates().Where(x => x.ProductID == productId).Average(x => x.RatePoint);
        }

        public static int GetRatePoint(int point, int productId)
        {
            int count = GetRates().Where(x => x.ProductID == productId).Count();
            if (count == 0)
                return 0;
            return GetRates().Where(x => x.ProductID == productId && x.RatePoint == point).Count() * 100 / count;
        }

        public static int? GetRatePoint(string userId, int productId)
        {
            var rate = GetRateByID(userId, productId);
            if (rate != null)
                return rate.RatePoint;
            return 0;
        }

        public static RateView GetRateView(int productId)
        {
            RateView rateView = new RateView();
            rateView.RatePoint = GetRatePoint(productId);
            rateView.PercentPoint = new List<int>();
            for (int i = 0; i < 5; i++)
                rateView.PercentPoint.Add(GetRatePoint(i + 1, productId));
            return rateView;
        }

        public static bool AddRate(Rate rate)
        {
            try
            {
                var oldRate = GetRates().SingleOrDefault(x => x.CreateBy == rate.CreateBy && x.ProductID == rate.ProductID);
                if (oldRate != null)
                {
                    oldRate.RatePoint = rate.RatePoint;
                    oldRate.CreateDate = DateTime.Now;
                    return UpdateRate(oldRate);
                }
                rate.CreateDate = DateTime.Now;
                db.Rates.Add(rate);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool AddRate(string userId, int productId, int ratePoint)
        {
            try
            {
                
                Rate rate = new Rate();
                rate.CreateBy = UserHelper.GetPropertyValue(userId, x => x.ID);
                rate.ProductID = productId;
                var oldRate = GetRates().SingleOrDefault(x => x.CreateBy == rate.CreateBy && x.ProductID == rate.ProductID);
                if (oldRate != null)
                {
                    oldRate.RatePoint = ratePoint;
                    oldRate.CreateDate = DateTime.Now;
                    return UpdateRate(oldRate);
                }
                rate.RatePoint = ratePoint;
                rate.CreateDate = DateTime.Now;
                db.Rates.Add(rate);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool DeleteRate(int id)
        {
            try
            {
                var rate = GetRateByID(id);
                if (rate != null)
                {
                    db.Rates.Remove(rate);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public static bool DeleteRate(Rate rate)
        {
            try
            {
                var oldRate = GetRateByID(rate.ID);
                if (oldRate != null)
                {
                    db.Rates.Remove(oldRate);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public static bool UpdateRate(Rate rate)
        {
            try
            {
                var oldRate = GetRateByID(rate.ID);
                if (oldRate != null)
                {
                    rate.CreateDate = DateTime.Now;
                    db.Rates.AddOrUpdate(x => x.ID, rate);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }

    public class CommentHelper : Helper
    {
        public static IEnumerable<Comment> GetComments()
        {
            return db.Comments.ToList();
        }

        public static IEnumerable<Comment> GetComments(string searchString)
        {
            return Search(GetComments(), searchString);
        }

        public static Comment GetCommentByID(int id)
        {
            return GetComments().SingleOrDefault(x => x.ID == id);
        }

        public static IEnumerable<TKey> GetPropertyValue<TKey>(Func<Comment, TKey> keySelector)
        {
            return GetPropertyValue(GetComments(), keySelector);
        }

        public static TKey GetPropertyValue<TKey>(int id, Func<Comment, TKey> keySelector)
        {
            return keySelector(GetCommentByID(id));
        }

        public static IEnumerable<CommentView> GetCommentView(int productId)
        {
            return GetComments().Where(x => x.ProductID == productId && !x.ParentID.HasValue).OrderByDescending(x => x.CreateDate).Select(x => new CommentView()
            {
                Comment = x,
                Name = UserHelper.GetPropertyValue((int)x.CreateBy, y => y.Name),
                Image = UserHelper.GetPropertyValue((int)x.CreateBy, y => y.Image),
                ReplyComment = GetComments().Where(y => y.ProductID == productId && y.ParentID == x.ID).OrderByDescending(y => y.CreateDate).Select(y => new CommentView()
                {
                    Comment = y,
                    Name = UserHelper.GetPropertyValue((int)y.CreateBy, z => z.Name),
                    Image = UserHelper.GetPropertyValue((int)y.CreateBy, z => z.Image),
                }).ToList()
            });
        }

        public static bool AddComment(Comment comment)
        {
            try
            {
                comment.CreateDate = DateTime.Now;
                db.Comments.Add(comment);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool AddComment(int productId, int createBy, string content, int? parentId = null)
        {
            try
            {
                Comment comment = new Comment();
                comment.ProductID = productId;
                comment.CreateBy = createBy;
                comment.Content = content;
                comment.ParentID = parentId;
                comment.CreateDate = DateTime.Now;
                db.Comments.Add(comment);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool DeleteComment(int id)
        {
            try
            {
                var comment = GetCommentByID(id);
                if (comment != null)
                {
                    db.Comments.Remove(comment);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public static bool DeleteComment(Comment comment)
        {
            try
            {
                var oldComment = GetCommentByID(comment.ID);
                if (oldComment != null)
                {
                    db.Comments.Remove(oldComment);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public static bool UpdateComment(Comment comment)
        {
            try
            {
                var oldComment = GetCommentByID(comment.ID);
                if (oldComment != null)
                {
                    comment.LastModify = DateTime.Now;
                    db.Comments.AddOrUpdate(x => x.ID, comment);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}