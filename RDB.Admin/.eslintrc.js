module.exports = {
  parserOptions: {
    ecmaVersion: 8,
  },
  extends: [
    'airbnb-base',
    'plugin:vue/recommended'
  ],
  rules: {
    'comma-dangle': ['error', 'never'], // trailing commas are unusual
    'eqeqeq': 'off', // too strict
    'linebreak-style': 'off', // git autocrlf on win
    'no-bitwise': ['error', { 'allow': ['~'] }], // allow ~indexOf
    'no-param-reassign': ['error', { 'props': false }], // allow vuex state mutations
    'no-plusplus': 'off', // too strict
    'semi': ['error', 'never'], // we don't need semicolons
  }
};
