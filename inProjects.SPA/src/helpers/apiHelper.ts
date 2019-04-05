// import AuthService from '../services/AuthService'
import axios from 'axios';

export function postAsync(url: string, data: any) {
    axios.post(url, data)
      .then((response) => {
        return response;
      })
      .catch((error) => {
        return error;
      });
}

export function putAsync(url: string, data: any) {
    axios.put(url, data)
      .then((response) => {
        return response;
      })
      .catch((error) => {
        return error;
      });
}

export function getAsync(url: string) {
    axios.get(url)
      .then((response) => {
        return response;
      })
      .catch((error) => {
        return error;
      });
}

export function deleteAsync(url: string) {
    axios.delete(url)
      .then((response) => {
        return response;
      })
      .catch((error) => {
        return error;
      });
}
