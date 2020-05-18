#include "io.h"
#include <stdio.h>

nil_t _MwriteStr_S0(string_t* str) {
	printf("%s\n", str->chars);
	return nil;
}
