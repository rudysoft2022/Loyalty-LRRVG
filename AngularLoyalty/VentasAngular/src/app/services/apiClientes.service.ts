import { Injectable } from '@angular/core';

import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Cliente } from '../Interface/models';
import { Login } from '../Interface/models';
@Injectable({
  providedIn: 'root'
})
export class ApiClientesService {

  private endpoint:string=environment.endPoint;
  private apiURL:string=this.endpoint+"api/Clientes/";


  constructor(private http:HttpClient) { }
  
  getClientes():Observable<Cliente[]>{
    return this.http.get<Cliente[]>(`${this.apiURL}GetClientes`);
    }
    getClientesById(IdCliente:number):Observable<Cliente>{
      return this.http.get<Cliente>(`${this.apiURL}GetClientes?input=${IdCliente}`);
      }
  
      creaActualizaClientes(modelo:Cliente):Observable<Cliente>{
      return this.http.post<Cliente>(`${this.apiURL}CreaActualizaClientes`,modelo);
    }
    
    bajaClientes(IdCliente:number):Observable<void>{
      return this.http.delete<void>(`${this.apiURL}BajaClientes?input=${IdCliente}`);
    }

    login(modelo:Login):Observable<Cliente>{
      return this.http.post<Cliente>(`${this.apiURL}LoginCliente`,modelo);
    }
}
