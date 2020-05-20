#include "core.h"
#include <string.h>
#include <stdio.h>

/* MEMORY */

/* Defined by the GC */
extern void* alloc(size_t len);
extern void* alloc_atomic(size_t len);
extern bool GC_init(void);

void mem_copy(void* from, void* to, size_t len) {
	memcpy(to, from, len);
}

/* STRINGS */

string_t* string_new_literal(char* literal, size_t len) {
	string_t* str = (string_t*)alloc_atomic(sizeof(string_t));
	str->chars = literal;
	str->length = len;
	return str;
}

string_t* string_concat(string_t* one, string_t* two) {
	string_t* str = (string_t*)alloc_atomic(sizeof(string_t));
	str->length = one->length + two->length;
	str->chars = (char*)alloc(str->length + 1);
	mem_copy(one->chars, str->chars, one->length);
	mem_copy(two->chars, str->chars + one->length, two->length);
	str->chars[str->length] = 0;
	return str;
}

/* LISTS */

list_t* list_new(void) {
	return (list_t*)NULL;
}

list_t* list_new1(size_t one) {
	list_t* list = (list_t*)alloc(sizeof(list_t));
	list->head = one;
	list->tail = list_new();
	return list;
}

list_t* list_new2(size_t one, size_t two) {
	list_t* list = list_new1(one);
	list->tail = list_new1(two);
	return list;
}

list_t* list_new3(size_t one, size_t two, size_t three) {
	list_t* list = list_new1(one);
	list->tail = list_new1(two);
	list->tail->tail = list_new1(three);
	return list;
}

list_t* list_new4(size_t one, size_t two, size_t three, size_t four) { 
	list_t* list = list_new1(one);
        list->tail = list_new1(two);
        list->tail->tail = list_new1(three);
	list->tail->tail->tail = list_new1(four);
	return list;
}

list_t* list_cons(size_t head, list_t* tail) {
	list_t* list = list_new1(head);
	list->tail = tail;
	return list;
}

size_t list_head(list_t* list) {
	return list->head;
}

list_t* list_tail(list_t* list) {
	return list->tail;
}

size_t list_at(list_t* list, size_t n) {
	if (n == 0) return list->head;
	return list_at(list->tail, n - 1);
}

list_t* list_tail_at(list_t* list, size_t n) {
	if (n == 0) return list;
	else if (n == 1) return list->tail;
	return list_tail_at(list->tail, n - 1);
}

size_t list_len(list_t* list) {
	if (list->tail == (list_t*)NULL) return 1;
	else return 1 + list_len(list->tail);
}

bool list_is_empty(list_t* list) {
	return list == (list_t*)NULL;
}

bool list_has_n(list_t* list, size_t n) {
	if (list == (list_t*)NULL) {
		if (n == 0) return true;
		else return false;
	} else if (n == 0) return true;
	else return list_has_n(list->tail, n - 1);
}

