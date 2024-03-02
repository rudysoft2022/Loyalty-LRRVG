export interface Cliente{
     idCliente:number;
     nombre :string;
     apellidos :string;
     direccion :string;
     correo :string;
     password :string;
    
}
export interface Login{
    correo:string;
    password:string;
}
export interface Tiendas{
    idTienda:number;
    sucursal:string;
    direccion:string;
    articulos:Articulos[];
}
export interface Articulos{
    codigo:string;
    descripcion:string;
    precio:number;
    stock?:number;
}
export interface Ventas{
    idTienda:number;
    idCliente:number;
    articulos:DetalleVenta[];
}
export interface DetalleVenta{
    codigo:number;
    precio:number;
    cantidad:number;
    descripcion:string;
}