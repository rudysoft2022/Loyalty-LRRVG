import { Injectable } from '@angular/core';

import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Tiendas } from '../Interface/models';
@Injectable({
  providedIn: 'root'
})
export class ApiTiendasService {

  private endpoint:string=environment.endPoint;
  private apiURL:string=this.endpoint+"api/Tiendas/";


  constructor(private http:HttpClient) { }
  
  getTiendas():Observable<Tiendas[]>{
    return this.http.get<Tiendas[]>(`${this.apiURL}GetTiendas`);
    }
    getTiendasById(IdTienda:number):Observable<Tiendas>{
      return this.http.get<Tiendas>(`${this.apiURL}GetTiendasById?input=${IdTienda}`);
      }
  
      creaActualizaTiendas(modelo:Tiendas):Observable<Tiendas>{
      return this.http.post<Tiendas>(`${this.apiURL}CreaActualizaTiendas`,modelo);
    }
    
    bajaTiendas(IdTienda:number):Observable<void>{
      return this.http.delete<void>(`${this.apiURL}BajaTiendas?input=${IdTienda}`);
    }

}
