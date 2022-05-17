using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAdmin.Models.SendoData
{
    public class Sendo
    {
    }
    public class UpdateStatusOrder
    {
        public string order_number { get; set; }
        public int order_status { get; set; }
        public string cancel_order_reason { get; set; }
    }
    public class Login
    {
        public string shop_key { get; set; } = "787462a7fe32467f86c90271f72a1256";
        public string secret_key { get; set; } = "0c78afe4f92a40f583ece5f227854799";
    }

    public class PageOrder
    {
        public int page_size { get; set; }
        public int order_status { get; set; }
        public DateTime? order_date_from { get; set; }
        public DateTime? order_date_to { get; set; }
        public DateTime? order_status_date_from { get; set; }
        public DateTime? order_status_date_to { get; set; }
    }
    public class Page
    {
        public int page_size { get; set; }
        public string product_name { get; set; }
        public DateTime? date_from { get; set; }
        public DateTime? date_to { get; set; }
        public int? status { get; set; }

    }
    public class ProductUpdate
    {
        public int product_id { get; set; }

        public decimal price { get; set; }

        public bool stock_availability { get; set; }


        public DateTime? promotion_from_date { get; set; }
        public DateTime? promotion_to_date { get; set; }

        public Variant Variant { get; set; }

    }
    public class ProductCreate
    {
        public int id { get; set; }
        public string name { get; set; }
        public string sku { get; set; }
        public decimal price { get; set; }
        public int weight { get; set; }
        public bool stock_availability { get; set; }

        public string description { get; set; }
        public int cat_4_id { get; set; }
        public string tags { get; set; }
        public List<Related> relateds { get; set; } = new List<Related>();
        public string seo_keyword { get; set; }
        public string seo_title { get; set; }
        public string seo_description { get; set; }
        public string product_image { get; set; }
        public int brand_id { get; set; }
        public string video_links { get; set; }
        public int height { get; set; }
        public int length { get; set; }
        public int width { get; set; }
        public int unit_id { get; set; }
        public int stock_quantity { get; set; }
        public Avatar avatar { get; set; }
        public List<Picture> pictures { get; set; } = new List<Picture>();
        public List<Certificate_file> certificate_file { get; set; } = new List<Certificate_file>();
        public List<Attribute> attributes { get; set; } = new List<Attribute>();
        public DateTime? promotion_from_date { get; set; }
        public DateTime? promotion_to_date { get; set; }
        public bool is_promotion { get; set; }
        public Extended_Shipping_Package extended_shipping_package { get; set; }
        public Variant Variant { get; set; }
        public bool is_config_variant { get; set; }
        public decimal special_price { get; set; }
        public Voucher voucher { get; set; }
    }
    public class Voucher
    {
        public int product_type { get; set; }
        public DateTime? start_date { get; set; }
        public DateTime? end_date { get; set; }
        public bool? is_check_date { get; set; }

    }
    public class Extended_Shipping_Package
    {
        public bool is_using_instant { get; set; }
        public bool is_using_in_day { get; set; }
        public bool is_self_shipping { get; set; }
        public bool is_using_standard { get; set; }
        public bool is_using_eco { get; set; }

    }
    public class Variant
    {
        public List<Variant_Attribute> variant_attributes { get; set; } = new List<Variant_Attribute>();
        public string variant_sku { get; set; }
        public decimal variant_price { get; set; }
        public DateTime? variant_promotion_start_date { get; set; }
        public DateTime? variant_promotion_end_date { get; set; }
        public decimal variant_special_price { get; set; }
        public int variant_quantity { get; set; }

    }
    public class Variant_Attribute
    {
        public int attribute_id { get; set; }
        public string attribute_code { get; set; }
        public int option_id { get; set; }

    }
    public class Certificate_file
    {
        public int id { get; set; }
        public string table_name { get; set; }
        public int table_id { get; set; }
        public string file_name { get; set; }
        public string attachment_url { get; set; }
        public int display_order { get; set; }

    }
    public class Attribute
    {
        public int attribute_id { get; set; }
        public string attribute_name { get; set; }
        public string attribute_code { get; set; }
        public bool attribute_is_custom { get; set; }
        public bool attribute_is_checkout { get; set; }
        public List<Attribute_value> attribute_values = new List<Attribute_value>();


    }
    public class Attribute_value
    {
        public int id { get; set; }
        public string value { get; set; }
        public string attribute_img { get; set; }
        public bool is_selected { get; set; }
        public bool is_custom { get; set; }

    }
    public class Picture
    {
        public string picture_url { get; set; }
    }
    public class Avatar
    {
        public string picture_url { get; set; }
    }
    public class Related
    {
        public int id { get; set; }
        public string name { get; set; }
        public string sku { get; set; }
        public decimal price { get; set; }
        public int status { get; set; }
        public string image { get; set; }
    }
}