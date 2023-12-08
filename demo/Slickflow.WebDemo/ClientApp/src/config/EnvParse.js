
const DotEnv = require('dotenv')
console.log('环境---' + process.env.NODE_ENV)
const parsedEnv = DotEnv.config({ path: '.env.development' }).parsed
console.log('环境---' + parsedEnv)
module.exports = function () {
  return parsedEnv
}
