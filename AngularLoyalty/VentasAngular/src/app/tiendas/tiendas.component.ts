import {AfterViewInit, Component, ViewChild, OnInit} from '@angular/core';
import {MatPaginator} from '@angular/material/paginator';
import {MatTableDataSource} from '@angular/material/table';
import { Tiendas } from '../Interface/models';
import { ApiTiendasService } from '../services/apiTiendas.service';
import {MatDialog} from '@angular/material/dialog';
import { AltaTiendasComponent } from './modales/alta-tiendas.component';
import { MatSnackBar } from '@angular/material/snack-bar';
import { BajaTiendasComponent } from './modales/baja-tiendas.component';

@Component({
  selector: 'app-tiendas',
  templateUrl: './tiendas.component.html',
  styleUrls: ['./tiendas.component.css']
})
export class TiendasComponent implements AfterViewInit,OnInit {

  displayedColumns: string[] = ['idTienda', 'sucursal', 'direccion', 'Acciones'];
  dataSource = new MatTableDataSource<Tiendas>();
  constructor(
    private _apisServicio:ApiTiendasService,
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
    this._apisServicio.getTiendas().subscribe({
      next:(dataResponse)=>{
        console.log(dataResponse);
          this.dataSource.data=dataResponse;
      },
      error:(e)=>{}
    })
  }

  nuevo() {
    const dialogRef = this.dialog.open(AltaTiendasComponent,{
      width:"350px"
    }).afterClosed().subscribe(resultado=>{
      console.log(`Dialog result: ${resultado}`);
      if(resultado==="creado"){
        this.mostrar();
      }
    });
  }

  editar(data:Tiendas) {
    const dialogRef = this.dialog.open(AltaTiendasComponent,{
      width:"350px",
      data:data
    }).afterClosed().subscribe(resultado=>{
      console.log(`Dialog result: ${resultado}`);
      if(resultado==="editado"){
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
  eliminar(data:Tiendas){
    const dialogRef = this.dialog.open(BajaTiendasComponent,{
      width:"350px",
      data:data
    }).afterClosed().subscribe(resultado=>{
      console.log(`Dialog result: ${resultado}`);
      if(resultado==="eliminado"){
        this._apisServicio.bajaTiendas(data.idTienda).subscribe({
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

