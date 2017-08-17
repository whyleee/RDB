import axios from 'axios'

axios.defaults.headers.post['Content-Type'] = 'application/json'
axios.defaults.headers.put['Content-Type'] = 'application/json'

const baseUrl = window.apiUrl

export default {
  values: {
    url: baseUrl + 'values',
    async get(id) {
      return await axios.get(this.url + (id ? `/${id}` : ''))
    },
    async post(text) {
      return await axios.post(this.url, JSON.stringify(text))
    },
    async put(id, text) {
      return await axios.put(`${this.url}/${id}`, JSON.stringify(text))
    },
    async delete(id) {
      return await axios.delete(`${this.url}/${id}`)
    }
  }
}
