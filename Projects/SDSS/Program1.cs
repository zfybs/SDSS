using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace SerializeCollection
{
    class Program1
    {
        static void Main(string[] args)
        {

            string filename = "e://books.xml";

            // Creates a new XmlSerializer.    
            XmlSerializer s =
                new XmlSerializer(typeof(MyRootClass));

            // Writing the file requires a StreamWriter.    
            TextWriter myWriter = new StreamWriter(filename);

            // Creates an instance of the class to serialize.     
            MyRootClass myRootClass = new MyRootClass();

            myRootClass.Items = CreateItemCollection();

            /* Serializes the class, writes it to disk, and closes    
           the TextWriter. */
            s.Serialize(myWriter, myRootClass);
            myWriter.Close();

            // retrive the object from the xml file.
            FileStream reader = new FileStream(filename, FileMode.Open, FileAccess.Read);
            XmlSerializer sReader = new XmlSerializer(typeof(MyRootClass));
            MyRootClass obj = sReader.Deserialize(reader) as MyRootClass;

        }

        private static List<Item> CreateItemCollection()
        {
            List<Item> items = new List<Item>();
            /* Uses a more advanced method of creating an list:   
        create instances of the Item and BookItem, where BookItem    
        is derived from Item. */
            Item item1 = new Item();
            // Sets the objects' properties.    
            item1.ItemName = "Widget1";
            item1.ItemCode = "w1";
            item1.ItemPrice = 231;
            item1.ItemQuantity = 3;

            BookItem bookItem = new BookItem();
            // Sets the objects' properties.    
            bookItem.ItemCode = "w2";
            bookItem.ItemPrice = 123;
            bookItem.ItemQuantity = 7;
            bookItem.ISBN = "34982333";
            bookItem.Title = "Book of Widgets";
            bookItem.Author = "John Smith";

            items.Add(item1);
            items.Add(bookItem);
            return items;
        }
    }

    // This is the class that will be serialized.    
    [Serializable]
    public class MyRootClass
    {
        public MyRootClass()
        {
            Items = new List<Item>();
        }

        /// <summary>
        /// 1. 在 Items 集合中，既可以添加Item对象，也可以添加其派生类BookItem对象。
        /// 2. 但是，如果要将此集合正常序列化，则必须通过 XmlArrayItemAttribute 特性将这两个类都进行标记，否则在序列化时会出现报错。在 XmlArrayItemAttribute 中，必须要通过 Type 来指定类型，而其他的属性（如 Namespace ）都不是必须的。
        /// 3. 如果不想通过 XmlArrayItemAttribute 进行标记，可以对基类 Item 的定义中进行“[XmlInclude(typeof(BookItem))]”标记。这两种方法任选其一，推荐XmlInclude标记的方法。
        /// </summary>
        [XmlArrayItem(ElementName = "Item", IsNullable = true, Type = typeof(Item), Namespace = "http://www.aboutdnn.com"),
         XmlArrayItem(ElementName = "BookItem", IsNullable = true, Type = typeof(BookItem), Namespace = "http://www.aboutdnn.com")]
        public List<Item> Items;

    }

    // 如果在上面的Items集合属性中应用了 XmlArrayItem标记，则这里可以不添加 XmlInclude 标记。
    [Serializable(), XmlInclude(typeof(BookItem))]
    public class Item
    {
        [XmlElement(ElementName = "OrderItem")]
        public string ItemName;
        public string ItemCode;
        public decimal ItemPrice;
        public int ItemQuantity;
    }

    [Serializable]
    public class BookItem : Item
    {
        public string Title;
        public string Author;
        public string ISBN;
    }
}