using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAdmin.Models.GiaoHangTietKiem
{
    public class fee
    {
        public string pick_province { get; set; }
        public string pick_district { get; set; }
        public string province { get; set; }
        public string district { get; set; }
        public string address { get; set; }
        public string transport { get; set; }
        public string deliver_option { get; set; }
        public string pick_ward { get; set; }
        public string pick_street { get; set; }

        public string pick_address { get; set; }
        public string ward { get; set; }


        public int? width { get; set; }
        public int? value { get; set; }

        public List<int> tags { get; set; } = new List<int>();

    }
    public class GiaoHangTietKiem
    {
    }
    public class category
    {
        public string level1 { get; set; }

    }

    public class TicketReply
    {
        public int? ticket_id { get; set; }

        public IFormFile attachments { get; set; }
        public string description { get; set; }
    }
    public class Ticket
    {
        public string c_email { get; set; }
        public string order_code { get; set; }
        public string category { get; set; }
        public IFormFile attachments { get; set; }
        public string description { get; set; }
    }
    public class user
    {
        public string username { get; set; }

    }
    public class Register
    {
        public string phone { get; set; }
        public string name { get; set; }
        public string ward_code { get; set; }
        public int? district_id { get; set; }
        public string address { get; set; }

    }
    public class Calculate_Fee
    {
        public int? from_district_id { get; set; }
        public int? service_id { get; set; }
        public int? service_type_id { get; set; }
        public int? to_district_id { get; set; }
        public string to_ward_code { get; set; }
        public int? height { get; set; }
        public int? length { get; set; }
        public int? weight { get; set; }
        public int? width { get; set; }
        public int? insurance_value { get; set; }
        public string coupon { get; set; }
    }
    public class get_service
    {
        public int? shop_id { get; set; }
        public int? from_district { get; set; }
        public int? to_district { get; set; }
    }
    public class item
    {
        public string name { get; set; }
        public string code { get; set; }
        public int? quantity { get; set; }
        public int? price { get; set; }
        public int? length { get; set; }
        public int? width { get; set; }
        public int? height { get; set; }
        public category category { get; set; }
    }
    public class Leadtime
    {
        public string to_ward_code { get; set; }
        public int? from_district_id { get; set; }
        public int? to_district_id { get; set; }
        public int? service_id { get; set; }
    }
    public class Preview
    {

        public int payment_type_id { get; set; }
        public string note { get; set; }
        public string required_note { get; set; }
        public string return_phone { get; set; }
        public string return_address { get; set; }
        public int? return_district_id { get; set; }
        public string return_ward_code { get; set; }
        public string client_order_code { get; set; }
        public string to_name { get; set; }
        public string to_phone { get; set; }
        public string to_address { get; set; }
        public string to_ward_code { get; set; }
        public int? to_district_id { get; set; }
        public int? cod_amount { get; set; }
        public string content { get; set; }
        public int? weight { get; set; }
        public int? length { get; set; }
        public int? width { get; set; }
        public int? height { get; set; }
        public int? pick_station_id { get; set; }
        public int? insurance_value { get; set; }
        public int? service_id { get; set; }
        public int? service_type_id { get; set; }
        public int? coupon { get; set; }
    }
    public class OrderCreate
    {
        public int payment_type_id { get; set; }
        public string note { get; set; }
        public string required_note { get; set; }
        public string return_phone { get; set; }
        public string return_address { get; set; }
        public int? return_district_id { get; set; }
        public string return_ward_code { get; set; }
        public string client_order_code { get; set; }
        public string to_name { get; set; }
        public string to_phone { get; set; }
        public string to_address { get; set; }
        public string to_ward_code { get; set; }
        public int? to_district_id { get; set; }
        public int? cod_amount { get; set; }
        public string content { get; set; }
        public int? weight { get; set; }
        public int? length { get; set; }
        public int? width { get; set; }
        public int? height { get; set; }
        public int? pick_station_id { get; set; }
        public int? deliver_station_id { get; set; }
        public int? insurance_value { get; set; }
        public int? service_id { get; set; }
        public int? service_type_id { get; set; }
        public int? order_value { get; set; }
        public int? coupon { get; set; }
        public List<int> pick_shift { get; set; } = new List<int>();
        public List<item> items { get; set; } = new List<item>();
    }

    public class order_codesid
    {
        public List<String> order_codes { get; set; } = new List<String>();
    }
    public class UpdateCOD
    {
        public string order_code { get; set; }
        public int? cod_amount { get; set; }
    }
    public class Station
    {
        public string note { get; set; }
        public string required_note { get; set; }
        public string return_phone { get; set; }
        public string return_address { get; set; }
    }

    public class OrderUpdate
    {
        public string note { get; set; }
        public string required_note { get; set; }
        public string return_phone { get; set; }
        public string return_address { get; set; }
        public int? return_district_id { get; set; }
        public string return_ward_code { get; set; }
        public string client_order_code { get; set; }
        public string to_name { get; set; }
        public string to_phone { get; set; }
        public string to_address { get; set; }
        public string to_ward_code { get; set; }
        public int? to_district_id { get; set; }
        public int? cod_amount { get; set; }
        public string content { get; set; }
        public int? weight { get; set; }
        public int? length { get; set; }
        public int? width { get; set; }
        public int? height { get; set; }
        public int? pick_station_id { get; set; }
        public int? deliver_station_id { get; set; }
        public int? insurance_value { get; set; }
        public int? service_id { get; set; }
        public int? service_type_id { get; set; }
        public int? order_value { get; set; }
        public int? coupon { get; set; }
        public List<int> pick_shift { get; set; } = new List<int>();
        public List<item> items { get; set; } = new List<item>();
        public string order_code { get; set; }
    }

    public class product
    {
        public int? quantity { get; set; }
        public string name { get; set; }
        public double? weight { get; set; }
        public int? product_code { get; set; }

    }
    public class order
    {
        public string id { get; set; }
        public string pick_name { get; set; }
        public string pick_address { get; set; }
        public string pick_province { get; set; }
        public string pick_district { get; set; }
        public string pick_ward { get; set; }
        public string pick_tel { get; set; }

        public string tel { get; set; }

        public string name { get; set; }
        public string address { get; set; }
        public string province { get; set; }

        public string district { get; set; }

        public string ward { get; set; }

        public string hamlet { get; set; }
        public string is_freeship { get; set; }
        public string pick_date { get; set; }

        public int? pick_money { get; set; }

        public string note { get; set; }

        public int? value { get; set; }
        public string transport { get; set; }
        public string pick_option { get; set; }

        public string deliver_option { get; set; }

        public int? pick_session { get; set; }

        public List<int> tags { get; set; } = new List<int>();

    }


    // giao hang tiet kiem
    public class CreateOrder
    {
        public List<product> products { get; set; } = new List<product>();
        public order order { get; set; }

    }
    public class CreateAccount
    {
        public string name { get; set; }
        public string first_address { get; set; }
        public string province { get; set; }
        public string district { get; set; }
        public string email { get; set; }
        public string tel { get; set; }
    }
    public class UseAccount
    {
        public string email { get; set; }
        public string password { get; set; }

    }
   
    public class XFAST
    {
        public string customer_district { get; set; }
        public string customer_province { get; set; }
        public string customer_ward { get; set; }
        public string customer_first_address { get; set; }
        public string customer_hamlet { get; set; }
        public string pick_province { get; set; }
        public string pick_district { get; set; }
        public string pick_ward { get; set; }
        public string pick_street { get; set; }
    }
    public class AddressLevel4
    {
        public string province { get; set; }
        public string address { get; set; }
        public string district { get; set; }
        public string ward_street { get; set; }

    }
}