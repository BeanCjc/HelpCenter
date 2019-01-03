using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using System.Text;
using System.Linq;

namespace HelpCenter.Common
{
    public class JsonHelper
    {
        /// <summary>
        /// JSON序列化
        /// </summary>
        public static string JsonSerializer<T>(T t)
        {
            var ser = new DataContractJsonSerializer(typeof(T));

            var ms = new MemoryStream();

            ser.WriteObject(ms, t);

            string jsonString = Encoding.UTF8.GetString(ms.ToArray());

            ms.Close();

            return jsonString;
        }

        /// <summary>
        /// JSON反序列化
        /// </summary>
        public static T JsonDeserialize<T>(string jsonString)
        {
            var ser = new DataContractJsonSerializer(typeof(T));
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)ser.ReadObject(ms);
            return obj;
        }

        #region 将一个数据表转换成一个JSON字符串，在客户端可以直接转换成二维数组。

        /// <summary>
        /// 返回JSON数据到前台
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <returns>JSON字符串</returns>
        public static string DataTableToJsonParam(DataTable dt)
        {
           // var javaScriptSerializer = new JavaScriptSerializer { MaxJsonLength = MaxValue };
            //取得最大数值
            var arrayList = new ArrayList();
            foreach (var dictionary in from DataRow dataRow in dt.Rows select dt.Columns.Cast<DataColumn>().ToDictionary<DataColumn, string, object>(dataColumn => dataColumn.ColumnName, dataColumn => dataRow[dataColumn.ColumnName].ToString()))
            {
                arrayList.Add(dictionary); //ArrayList集合中添加键值
            }

            
            return JsonConvert.SerializeObject(arrayList);  //返回一个json字符串
        }

        #endregion 将一个数据表转换成一个JSON字符串，在客户端可以直接转换成二维数组。
    }

    /*
     序列化Demo：

      简单对象Person：

      1: public class Person

      2: {
      3:     public string Name { get; set; }

      4:     public int Age { get; set; }

      5: }

      序列化为JSON字符串：

      1: protected void Page_Load(object sender, EventArgs e)

      2: {
      3:     Person p = new Person();

      4:     p.Name = "张三";

      5:     p.Age = 28;

      6:

      7:     string jsonString = JsonHelper.JsonSerializer<Person>(p);

      8:     Response.Write(jsonString);

      9: }

      输出结果：

      {"Age":28,"Name":"张三"}

      反序列化Demo：

      1: protected void Page_Load(object sender, EventArgs e)

      2: {
      3:     string jsonString = "{\"Age\":28,\"Name\":\"张三\"}";

      4:     Person p = JsonHelper.JsonDeserialize<Person>(jsonString);

      5: }

     */
}
