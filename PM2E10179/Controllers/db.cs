using PM2E10179.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PM2E10179.Controllers{
    public class db{
        readonly SQLiteAsyncConnection _con;
        
        public db() { }

        public db(string dbPath){
            _con = new SQLiteAsyncConnection(dbPath);
            _con.CreateTableAsync<sitios>().Wait();
        }

        public Task<int> addSite(sitios site){
            if (site.id == 0){
                return _con.InsertAsync(site);
            } else {
                return _con.UpdateAsync(site);
            }
        }

        public Task<List<sitios>> GetAll(){
            return _con.Table<sitios>().ToListAsync();
        }

        public Task<sitios> getById(int id){
            return _con.Table<sitios>().Where(i => i.id == id).
            FirstOrDefaultAsync();
        }

        public Task<int> deleteSite(sitios site){
            return _con.DeleteAsync(site);
        }
    }
}
