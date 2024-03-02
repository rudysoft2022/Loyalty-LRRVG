import {AfterViewInit, Component, ViewChild, OnInit} from '@angular/core';
import {MatPaginator} from '@angular/material/paginator';
import {MatTableDataSource} from '@angular/material/table';
import { Articulos } from '../Interface/models';
import { ApiArticulosService } from '../services/apiArticulos.service';
import {MatDialog} from '@angular/material/dialog';
import { AltaArticulosComponent } from './modales/alta-articulos.component';
import { MatSnackBar } from '@angular/material/snack-bar';
import { BajaArticulosComponent } from './modales/baja-articulos.component';

@Component({
  selector: 'app-articulos',
  templateUrl: './articulos.component.html',
  styleUrls: ['./articulos.component.css']
})
export class ArticulosComponent implements AfterViewInit,OnInit {

  displayedColumns: string[] = ['codigo', 'descripcion', 'precio','stock', 'Acciones'];
  dataSource = new MatTableDataSource<Articulos>();
  constructor(
    private _apisServicio:ApiArticulosService,
    public dialog: MatDialog,
    private _snackBar:MatSnackBar
  ){

  }

  ngOnInit(): void {
    this.mostrar();
  }

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }

  mostrar(){
    this._apisServicio.getArticulos().subscribe({
      next:(dataResponse)=>{
        console.log(dataResponse);
          this.dataSource.data=dataResponse;
      },
      error:(e)=>{}
    })
  }

  nuevo() {
    const dialogRef = this.dialog.open(AltaArticulosComponent,{
      width:"350px"
    }).afterClosed().subscribe(resultado=>{
      console.log(`Dialog result: ${resultado}`);
      if(resultado==="creado"){
        this.mostrar();
      }
    });
  }

  editar(data:Articulos) {
    const dialogRef = this.dialog.open(AltaArticulosComponent,{
      width:"350px",
      data:data
    }).afterClosed().subscribe(resultado=>{
      console.log(`Dialog result: ${resultado}`);
      if(resultado==="creado"){
        this.mostrar();
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
  eliminar(data:Articulos){
    const dialogRef = this.dialog.open(BajaArticulosComponent,{
      width:"350px",
      data:data
    }).afterClosed().subscribe(resultado=>{
      console.log(`Dialog result: ${resultado}`);
      if(resultado==="eliminado"){
        this._apisServicio.bajaArticulo(data.codigo).subscribe({
          next:(dataResponse)=>{
            //this.mostrarAlerta("Eliminado","listo");
            this.mostrar();
          },error:(e)=>{
            this.mostrarAlerta("No se elimino","Error");
          }
        })
      }
    });
  }

}

