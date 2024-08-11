import { TestBed } from '@angular/core/testing';

import { SuperPowerService } from './superpower.service';

describe('SuperPowerService', () => {
  let service: SuperPowerService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SuperPowerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
