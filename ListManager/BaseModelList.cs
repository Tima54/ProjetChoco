using Models;

namespace ListManager
{
    public class BaseModelList<T> where T : BaseModel
    {
        public List<T> list;

        public BaseModelList()
        {  
            list = new List<T>();
        }

        public bool setlList(List<T> list)
        {
            if(list != null)
            {
                this.list = list;
            }
            else
            {
                this.list = new List<T>();
            }
            return true;
        }

        public void Add(T user)
        {
            list.Add(user);
        }

        public void Remove(T user)
        {
            list.Remove(user);
        }

        public List<T> GetList()
        {
            return list;
        }

        public T GetById(Guid id)
        {
            return list.Find(u => u.Id == id)!;
        }

        public T GetByIndex(int index) 
        {
            return list[index];
        }
    }

}