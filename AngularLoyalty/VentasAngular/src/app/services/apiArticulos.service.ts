import { Injectable } from '@angular/core';

import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Articulos } from '../Interface/models';
@Injectable({
  providedIn: 'root'
})
export class ApiArticulosService {

  private endpoint:string=environment.endPoint;
  private apiURL:string=this.endpoint+"api/Articulos/";


  constructor(private http:HttpClient) { }
  
  getArticulos():Observable<Articulos[]>{
    return this.http.get<Articulos[]>(`${this.apiURL}GetArticulos`);
    }
    getArticuloById(Codigo:string):Observable<Articulos>{
      return this.http.get<Articulos>(`${this.apiURL}GetArticuloById?input=${Codigo}`);
      }
  
      creaActualizaArticulo(modelo:Articulos):Observable<Articulos>{
      return this.http.post<Articulos>(`${this.apiURL}CreaActualizaArticulo`,modelo);
    }
    
    bajaArticulo(Codigo:string):Observable<void>{
      return this.http.delete<void>(`${this.apiURL}BajaArticulo?input=${Codigo}`);
    }

}
