module.exports = {
  parserOptions: {
    ecmaVersion: 8,
  },
  extends: [
    'airbnb-base',
    'plugin:vue/recommended'
  ],
  rules: {
    'comma-dangle': ['error', 'never'],
    'eqeqeq': 'off',
    'linebreak-style': 'off', // git autocrlf on win
    'no-bitwise': ['error', { 'allow': ['~'] }],
    'no-param-reassign': ['error', { 'props': false }],
    'no-plusplus': 'off',
    'semi': ['error', 'never'],
  }
};
