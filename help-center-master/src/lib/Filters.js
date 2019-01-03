/**
 * 全局过滤器
 * @param Vue
 */

import moment from 'moment';

const install = function (Vue) {

    /**
     * 日期格式化【YYYY-MM-DD HH:mm:ss】
     */
    Vue.filter('datetimeFormat', function (val) {
        if (!val || val === 0) {
            return '';
        }
        return moment(val).format('YYYY-MM-DD HH:mm:ss');
    });

    /**
     * 日期格式化【YYYY-MM-DD HH:mm:ss】
     */
    Vue.filter('timestampFormat', function (val) {
        if (!val || val === 0) {
            return '';
        }
        return moment(val).format('YYYY-MM-DD HH:mm:ss.SSS');
    });

    /**
     * 日期格式化【YYYY-MM-DD】
     */
    Vue.filter('dateFormat', function (val) {
        if (!val || val === 0) {
            return '';
        }
        return moment(val).format('YYYY-MM-DD');
    });

    /**
     * 字符串长度格式化，最长15个字符，其余的以（...）展示
     */
    Vue.filter('textAbbreviate15', function (val) {
        if (val && val.length > 15) {
            return val.substring(0, 15) + "...";
        }
        return val;
    });

    /**
     * 交易金额格式化，将(分)转(元)
     */
    Vue.filter('tradeAmount', function (val) {
        if (val) {
            return val / 100;
        }
        return val;
    });

};

export default install;
