import axios from 'axios'
import config from '../config/index.js'

const state = {
  roles: null,
  users: null
}

const getters = {
  roles: state => {
    return state.roles
  },
  users: state => {
    return state.users
  }
}

const mutations = {
  setRoles: (state, payload) => {
    state.roles = payload
  },
  setUsers: (state, payload) => {
    state.users = payload
  }
}

const actions = {
  getRoles: (context, payload) => {
    var query = {}
    query.ProcessGUID = config.PROCESS_A4L.ProcessGUID
    query.Version = config.PROCESS_A4L.Version

    if (process.env.DEV) {
      console.log('I\'m on a development build')
    }
    console.log(process.env)

    console.log(process.env.VUE_APP_BASE_URL)
    axios.post(`${process.env.VUE_APP_BASE_URL}/wf/QueryProcessRoles`, query)
      .then((response) => {
        var roles = response.data.Entity
        context.commit('setRoles', roles)
      })
      .catch((error) => {
        window.console.log(error)
      })
  },
  getUsers: (context, payload) => {
    axios.get(`${process.env.VUE_APP_BASE_URL}/wf/GetUserByRole/` + payload)
      .then((response) => {
        var users = response.data.Entity
        context.commit('setUsers', users)
      })
      .catch((error) => {
        window.console.log(error)
      })
  }
}

export default {
  namespaced: true,
  state,
  mutations,
  actions,
  getters
}
