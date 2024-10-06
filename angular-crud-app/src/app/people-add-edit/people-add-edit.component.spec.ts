import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PeopleAddEditComponent } from './people-add-edit.component';

describe('PeopleAddEditComponent', () => {
  let component: PeopleAddEditComponent;
  let fixture: ComponentFixture<PeopleAddEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PeopleAddEditComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PeopleAddEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
