using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planejador_de_tarefas
{
    internal class Category
    {
        public string _nameCategory { get; set; }

        public Category(string Category)
        {
            _nameCategory = Category;
        }

        public string ToCategory()
        {
            if (_nameCategory == null)
            {
                return "vazio";
            }
            else {
                return $"{_nameCategory}";
            }
        }
    }
}