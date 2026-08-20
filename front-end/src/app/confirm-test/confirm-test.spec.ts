import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfirmTest } from './confirm-test';

describe('ConfirmTest', () => {
  let component: ConfirmTest;
  let fixture: ComponentFixture<ConfirmTest>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ConfirmTest],
    }).compileComponents();

    fixture = TestBed.createComponent(ConfirmTest);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
