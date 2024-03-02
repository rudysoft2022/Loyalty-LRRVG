import { Injectable } from '@angular/core';
import { DetalleVenta } from '../Interface/models';
import { Subject } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class CarritoService {

    items: DetalleVenta [] = [];
    cartChanged = new Subject<DetalleVenta[]>();
  
    constructor() { }
  
    getCartItems() {
      return this.items.slice();
    }
  
    deleteCartItem(index : number) {
      this.items.splice(index, 1);
      this.cartChanged.next(this.items.slice());
    }
  
    addCartItems(item: DetalleVenta) {
      this.items.push(item);
      this.cartChanged.next(this.items.slice());
    }

}
