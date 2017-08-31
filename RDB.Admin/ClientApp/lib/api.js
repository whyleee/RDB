import axios from 'axios'

axios.defaults.headers.post['Content-Type'] = 'application/json'
axios.defaults.headers.put['Content-Type'] = 'application/json'

const baseUrl = window.apiUrl

export default {
  values: {
    url: `${baseUrl}values`,
    get(id) {
      return axios.get(this.url + (id ? `/${id}` : ''))
    },
    post(text) {
      return axios.post(this.url, JSON.stringify(text))
    },
    put(id, text) {
      return axios.put(`${this.url}/${id}`, JSON.stringify(text))
    },
    delete(id) {
      return axios.delete(`${this.url}/${id}`)
    }
  }
}
