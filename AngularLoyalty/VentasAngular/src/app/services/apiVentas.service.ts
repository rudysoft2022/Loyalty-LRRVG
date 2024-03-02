import { Injectable } from '@angular/core';

import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Ventas } from '../Interface/models';
@Injectable({
  providedIn: 'root'
})
export class ApiVentasService {

  private endpoint:string=environment.endPoint;
  private apiURL:string=this.endpoint+"api/Ventas/";


  constructor(private http:HttpClient) { }
  

      realizaVenta(modelo:Ventas):Observable<Ventas>{
      return this.http.post<Ventas>(`${this.apiURL}RegistraVentas`,modelo);
    }
    

}
