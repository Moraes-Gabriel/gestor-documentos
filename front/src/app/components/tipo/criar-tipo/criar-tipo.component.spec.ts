import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CriarTipoComponent } from './criar-tipo.component';

describe('CriarTipoComponent', () => {
  let component: CriarTipoComponent;
  let fixture: ComponentFixture<CriarTipoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CriarTipoComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CriarTipoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
