/*
 * Code generated by Microsoft (R) AutoRest Code Generator.
 * Changes may cause incorrect behavior and will be lost if the code is
 * regenerated.
 */

import { RequestOptionsBase } from "ms-rest-js";


/**
 * @interface
 * An interface representing Value.
 * Represents a text value with ID.
 *
 */
export interface Value {
  /**
   * @member {string} [id] Value ID.
   */
  id?: string;
  /**
   * @member {string} [text] Value text.
   */
  text?: string;
}

/**
 * @interface
 * An interface representing RdbApiValuesPostOptionalParams.
 * Optional Parameters.
 *
 * @extends RequestOptionsBase
 */
export interface RdbApiValuesPostOptionalParams extends RequestOptionsBase {
  /**
   * @member {string} [text] New value text.
   */
  text?: string;
}

/**
 * @interface
 * An interface representing RdbApiValuesByIdPutOptionalParams.
 * Optional Parameters.
 *
 * @extends RequestOptionsBase
 */
export interface RdbApiValuesByIdPutOptionalParams extends RequestOptionsBase {
  /**
   * @member {string} [text] New value text.
   */
  text?: string;
}