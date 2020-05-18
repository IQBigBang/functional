#include "fmt.h"
#include <stdio.h>

string_t* _Mitoa_I0(int n) {
	char* buffer = (char*)alloc_atomic(19);
	int len = sprintf(buffer, "%d", n);
	return string_new_literal(buffer, len);
}
