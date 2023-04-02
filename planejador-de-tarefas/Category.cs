using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planejador_de_tarefas
{
    internal class Category
    {
        public string NameCategory { get; set; }

        public Category(string Category)
        {
            NameCategory = Category;
        }

        public string ToCategory()
        {
            if (NameCategory == null)
            {
                return "vazio";
            }
            else {
                return $"{NameCategory}";
            }
        }
    }
}