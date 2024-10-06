import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { PeopleService } from '../services/people.service';
import { CoreService } from '../core/core.service';

@Component({
  selector: 'app-people-add-edit',
  templateUrl: './people-add-edit.component.html',
  styleUrls: ['./people-add-edit.component.scss']
})
export class PeopleAddEditComponent implements OnInit {
  empForm!: FormGroup;
  title: string = 'Agregar Persona';

  constructor(
    private fb: FormBuilder,
    private _peopleService: PeopleService,
    private dialogRef: MatDialogRef<PeopleAddEditComponent>,
    private _coreService: CoreService,
    @Inject(MAT_DIALOG_DATA) public data: any // Inyectar los datos para edición
  ) {}

  ngOnInit(): void {
    // Definir el formulario reactivo
    this.empForm = this.fb.group({
      id: [null],
      firstName: ['', [Validators.required]],
      lastName: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      dateOfBirth: ['', Validators.required],
      phoneNumber: ['', [Validators.required, Validators.pattern('^[0-9]{10}$')]],
      street: [''],
      city: [''],
      postalCode: ['', [Validators.pattern('^[0-9]{5}$')]]
    });

    // Si hay datos (para edición), rellenar el formulario
    if (this.data) {
      this.title = 'Editar Persona';
      this.empForm.patchValue(this.data); // Cargar datos en el formulario para edición
    }
  }

  onFormSubmit(): void {
    if (this.empForm.valid) {
      console.log('Datos enviados:', this.empForm.value);  // Verifica qué datos se están enviando
      if (this.data) {
        this._peopleService.updatePeople(this.data.id, this.empForm.value).subscribe({
          next: (val: any) => {
            this._coreService.openSnackBar('Los datos de la persona se han actualizado.');
            this.dialogRef.close(true); // Cerrar el diálogo después de la actualización
          },
          error: (err: any) => {
            console.error('Error actualizando persona:', err);
          }
        });
      } else {
        this._peopleService.addPeople(this.empForm.value).subscribe({
          next: (val: any) => {
            this._coreService.openSnackBar('Persona agregada exitosamente.');
            this.dialogRef.close(true); // Cerrar el diálogo después de agregar
          },
          error: (err: any) => {
            console.error('Error agregando persona:', err);
          }
        });
      }
    } else {
      console.log('Formulario inválido', this.empForm);  // Depuración
    }
  }
  
}
