import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { accessTokenKey } from '../constants/constants';
import { ILoginParameters } from './../../models/login-parameters';
import { ILoginResponse } from './../../models/login-response';
import { baseURL } from './../constants/constants';
import { LocalStorageService } from './local-storage.service';

@Injectable()
export class AuthenticationService {
  constructor(
    @Inject(baseURL) private _baseUrl: string,
    private _httpClient: HttpClient,
    private _localStorageService: LocalStorageService
  ) {}

  public logout() {
    this._localStorageService.put({ name: accessTokenKey, value: null });
  }

  public tryToLogin(options: ILoginParameters): Observable<string> {
    return this._httpClient.post<ILoginResponse>(`${this._baseUrl}api/users/login`, options).pipe(
      map((response) => {
        this._localStorageService.put({ name: accessTokenKey, value: response.accessToken });
        return response.accessToken;
      })
    );
  }
}
