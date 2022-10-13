import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TabelaTipoComponent } from './tabela-tipo.component';

describe('TabelaTipoComponent', () => {
  let component: TabelaTipoComponent;
  let fixture: ComponentFixture<TabelaTipoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TabelaTipoComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TabelaTipoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
