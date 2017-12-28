using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueWay_Shangliao.Data
{
    class Product
    {
        /// <summary>
        /// 产品类型或工序位
        /// </summary>
        public enum ProductType
        {
            None,
            Product001,
            Product002,
            Product003,
            Product004,
            Product005,
            Product006,
        }
        
        private static ProductType curProduct = ProductType.Product001;//当前产品号
        private static Product productInstance = null;
        
        internal static ProductType CurProduct
        {
            get
            {
                return curProduct;
            }
            set
            {
                curProduct = value;
            }
        }
        
        private Product()
        {
        }
        public static Product GetProductInstance()
        {
            if(productInstance == null)
            {
                productInstance = new Product();
            }
            
            return productInstance;
        }
        
        public static ProductType GetCurProduct()
        {
            return CurProduct;
        }
    }
}
