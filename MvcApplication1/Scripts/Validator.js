
var validatorObj = {
    validatorFunctions: {
        "length": function(text, size) {
            return text.length >= size;
        },
        "email": function (text, size) {
            var count;
            var atCount = 0;
            for (count = 0; count < text.length; count++) {
                if (text.charAt(count) == '@') {
                    atCount++;
                }
            }
            return atCount == 1 && count >= size;
        }
    },
    

    validate : function(elemName, validType, extraParam) {
        return validatorObj.validatorFunctions[validType](elemName, extraParam);
    },
    addCond : function(validType,cond) {
        validatorObj.validatorFunctions[validType] = cond;
    }
};


