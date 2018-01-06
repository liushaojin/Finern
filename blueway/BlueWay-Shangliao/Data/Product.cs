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
        /// 产品类型或工序位,以便根据不同的工位初始化不同的界面
        /// </summary>
        public enum ProductType
        {
            None,
            Product,    //服务端
            Product001, //1#工位--上料
            Product002, //2#工位--
            Product003, //3#工位--
            Product004, //4#工位--
            Product005, //5#工位--
            Product006, //6#工位--
            Product007, //7#工位--
            Product008, //8#工位--
            Product009, //9#工位--
            Product010, //10#工位--
            Product011, //11#工位--
            Product012, //12#工位--
            Product013, //13#工位--
            Product014, //14#工位--
            Product015, //15#工位--
            Product016, //16#工位--
            Product017, //17#工位--
            Product018, //18#工位--
            Product019, //19#工位--
            Product020, //20#工位--
            Product021, //21#工位--
            Product022, //22#工位--
            Product023, //23#工位--
            Product024, //24#工位--
            Product025, //25#工位--
            Product026, //26#工位--
            Product027, //27#工位--
            Product028, //28#工位--
            Product029, //29#工位--
            Product030, //30#工位--
            Product031, //31#工位--
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
