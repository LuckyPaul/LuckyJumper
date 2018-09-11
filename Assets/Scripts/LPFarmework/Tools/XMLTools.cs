/***
 * 
 *    Title: LPFamework
 *           主题：       
 *    Description: 
 *           功能： 
 *                  
 *    Date: 9/11/2018
 *    Version: 0.1版本
 *    Modify Recoder: 
 *    Author: WSX
 *   
 */

using System.Xml;

namespace LuckyPual.Tools {
	public class XMLTools 
	{
        private  XmlDocument xmlDocumenter;
        public XmlDocument XmlDocumenter
        {
            get
            {
                if (xmlDocumenter == null)
                {
                    xmlDocumenter = new XmlDocument();
                }
                return xmlDocumenter;
            }
        }

        public XmlDocument OpenXML(string path)
        {
            XmlDocumenter.Load(path);
            return XmlDocumenter;

        }


    }
}

