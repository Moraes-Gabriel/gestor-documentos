import { TestBed } from '@angular/core/testing';

import { ConcessaoService } from './concessao.service';

describe('ConcessaoService', () => {
  let service: ConcessaoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ConcessaoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
