#include "std.h"

#include <string.h>
#include <stdio.h>

void mem_copy(void* from, void* to, size_t len) {
	memcpy(to, from, len);
}

nil_t _Std_WriteStr(string_t* str) {
	printf("%s\n", str->chars);
	return nil;
}

string_t* _Std_Itoa(int n) {
	char* buffer = (char*)alloc_atomic(19);
	int len = sprintf(buffer, "%d", n);
	return string_new_literal(buffer, len);
}
