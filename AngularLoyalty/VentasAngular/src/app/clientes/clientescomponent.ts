import {AfterViewInit, Component, ViewChild, OnInit} from '@angular/core';
import {MatPaginator} from '@angular/material/paginator';
import {MatTableDataSource} from '@angular/material/table';
import { Cliente } from '../Interface/models';
import { ApiClientesService } from '../services/apiClientes.service';
import {MatDialog} from '@angular/material/dialog';
import { AltaClienteComponent } from './modales/alta-cliente.component';
import { MatSnackBar } from '@angular/material/snack-bar';
import { BajaClienteComponent } from './modales/baja-cliente.component';

@Component({
  selector: 'app-clientes',
  templateUrl: './clientes.component.html',
  styleUrls: ['./clientes.component.css']
})
export class ClientesComponent implements AfterViewInit,OnInit {

  displayedColumns: string[] = ['idCliente', 'nombre', 'apellidos', 'direccion','correo','Acciones'];
  dataSource = new MatTableDataSource<Cliente>();
  constructor(
    private _apisServicio:ApiClientesService,
    public dialog: MatDialog,
    private _snackBar:MatSnackBar
  ){

  }

  ngOnInit(): void {
    this.mostrarCliente();
  }

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }

  mostrarCliente(){
    this._apisServicio.getClientes().subscribe({
      next:(dataResponse)=>{
        console.log(dataResponse);
          this.dataSource.data=dataResponse;
      },
      error:(e)=>{}
    })
  }

  nuevoCliente() {
    const dialogRef = this.dialog.open(AltaClienteComponent,{
      width:"350px"
    }).afterClosed().subscribe(resultado=>{
      console.log(`Dialog result: ${resultado}`);
      if(resultado==="creado"){
        this.mostrarCliente();
      }
    });
  }

  editarCliente(data:Cliente) {
    const dialogRef = this.dialog.open(AltaClienteComponent,{
      width:"350px",
      data:data
    }).afterClosed().subscribe(resultado=>{
      console.log(`Dialog result: ${resultado}`);
      if(resultado==="editado"){
        this.mostrarCliente();
      }
    });
  }

  mostrarAlerta(msg:string,accion:string){
    this._snackBar.open(msg,accion,{
      horizontalPosition:"end",
      verticalPosition:"top",
      duration:3000
    });
  }
  eliminarCliente(data:Cliente){
    const dialogRef = this.dialog.open(BajaClienteComponent,{
      width:"350px",
      data:data
    }).afterClosed().subscribe(resultado=>{
      console.log(`Dialog result: ${resultado}`);
      if(resultado==="eliminado"){
        this._apisServicio.bajaClientes(data.idCliente).subscribe({
          next:(dataResponse)=>{
            //this.mostrarAlerta("Eliminado","listo");
            this.mostrarCliente();
          },error:(e)=>{
            this.mostrarAlerta("No se elimino","Error");
          }
        })
      }
    });
  }

}

