import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { CoreService } from './core/core.service';
import { PeopleService } from './services/people.service';
import { PeopleAddEditComponent } from './people-add-edit/people-add-edit.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {

  displayedColumns: string[] = [
    'id',
    'firstName',
    'lastName',
    'email',
    'dateOfBirth',
    'phoneNumber',
    'street',
    'city',
    'postalCode',
    'action'

  ];
  
  dataSource!: MatTableDataSource<any>;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private _dialog: MatDialog,
    private _peopleService: PeopleService,
    private _coreService: CoreService
  ) {}

  ngOnInit(): void {
    this.getPeopleList();
  }

  // Método para abrir el formulario de agregar/editar empleado
  openAddEditEmpForm() {
    const dialogRef = this._dialog.open(PeopleAddEditComponent, {
      width: '400px',  // Ajusta el tamaño del diálogo si es necesario
      data: null  // No estamos pasando ningún dato cuando es un nuevo registro
    });
    
    dialogRef.afterClosed().subscribe({
      next: (val) => {
        if (val) {
          this.getPeopleList();  // Actualizar la lista de personas después de agregar
        }
      },
    });
  }

  // Método para obtener la lista de personas
  getPeopleList() {
    this._peopleService.getPeopleList().subscribe({
      next: (res) => {
        this.dataSource = new MatTableDataSource(res);
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
      },
      error: console.log,
    });
  }

  // Método para aplicar el filtro en la tabla
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  // Método para eliminar un empleado
  deleteEmployee(id: number) {
    this._peopleService.deletePeople(id).subscribe({
      next: (res) => {
        this._coreService.openSnackBar('Persona Eliminada!', 'Bien');
        this.getPeopleList();  // Actualizar la lista después de eliminar
      },
      error: console.log,
    });
  }

  // Método para abrir el formulario de edición con datos
  openEditForm(data: any) {
    const dialogRef = this._dialog.open(PeopleAddEditComponent, {
      width: '400px',
      data  // Pasar los datos del empleado para la edición
    });

    dialogRef.afterClosed().subscribe({
      next: (val) => {
        if (val) {
          this.getPeopleList();  // Actualizar la lista de personas después de editar
        }
      },
    });
  }
}
