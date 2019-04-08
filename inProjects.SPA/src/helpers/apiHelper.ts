// import AuthService from '../services/AuthService'
//import axios from 'axios';
import {getAxios} from '../modules/authService';


export function postAsync(url: string, data: any) {
  getAxios().post(url, data)
      .then((response) => {
        return response;
      })
      .catch((error) => {
        return error;
      });
}

export function putAsync(url: string, data: any) {
  getAxios().put(url, data)
      .then((response) => {
        return response;
      })
      .catch((error) => {
        return error;
      });
}

export function getAsync(url: string) {
  getAxios().get(url)
      .then((response) => {
        return response;
      })
      .catch((error) => {
        return error;
      });
}

export function deleteAsync(url: string) {
  getAxios().delete(url)
      .then((response) => {
        return response;
      })
      .catch((error) => {
        return error;
      });
}
