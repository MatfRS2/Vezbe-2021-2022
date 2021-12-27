import { Injectable } from '@angular/core';
import { LocalStorageKeys } from './local-storage-keys';

@Injectable({
  providedIn: 'root'
})
export class LocalStorageService {

  constructor() { }

  public set(key: LocalStorageKeys, value: any): void {
    localStorage.setItem(key, JSON.stringify(value));
  }

  public get(key: LocalStorageKeys): any | null {
    const value: string | null = localStorage.getItem(key);
    if (value === null) {
      return null;
    }
    return JSON.parse(value);
  }

  public clear(key: LocalStorageKeys): void {
    localStorage.removeItem(key);
  }
}
