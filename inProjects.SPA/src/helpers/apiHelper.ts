import {getAxios} from '../modules/authService'
import { AxiosResponse } from 'axios';

export  async function postAsync(url: string, data: any) {
  return getAxios().post(url, data)
      .then((response) => {
        return response
      })
      .catch((error) => {
        return error
      })
}

export async function putAsync(url: string, data: any) {
  return getAxios().put(url, data)
      .then((response) => {
        return response
      })
      .catch((error) => {
        return error
      })
}

export async function getAsync(url: string) {
  return getAxios().get(url)
      .then((response) => {
        return response
      })
      .catch((error) => {
        return error
      })
}

export async function deleteAsync(url: string) {
  return getAxios().delete(url)
      .then((response) => {
        return response
      })
      .catch((error) => {
        return error
      })
}
