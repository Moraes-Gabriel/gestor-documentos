import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TabelaConcessaoComponent } from './tabela-concessao.component';

describe('TabelaConcessaoComponent', () => {
  let component: TabelaConcessaoComponent;
  let fixture: ComponentFixture<TabelaConcessaoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TabelaConcessaoComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TabelaConcessaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
