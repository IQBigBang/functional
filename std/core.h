#ifndef FUNCTIONAL_CORE_LIB
#define FUNCTIONAL_CORE_LIB
#include <stddef.h>

/* std.core library - implicitly imported and linked in */

/* Core library types */

typedef unsigned char bool;
#define true 1
#define false 0

typedef enum { nil } nil_t;

typedef struct list_struct {
	size_t head;
	struct list_struct* tail;
} list_t;

typedef struct string_struct {
	char* chars;
	size_t length;
} string_t;

/* Core library functions */

/* Allocate `len` bytes */
void* alloc(size_t len);

/* Allocate `len` bytes that don't contain any pointers inside */
void* alloc_atomic(size_t len);

/* Initialize the garbage collector */
bool GC_init(void);

/* Copy n bytes of memory */
void mem_copy(void* from, void* to, size_t len);

/* Create a new string object from a string literal */
string_t* string_new_literal(char* literal, size_t len);

/* Concatenate two strings into a new string
 * Copies both of the strings
 * Time complexity: maximum O(m+n) where m and n are sizes of strings
 */
string_t* string_concat(string_t* one, string_t* two);

/* Create a new empty list
 * Time complexity: maximum O(1)
 */
list_t* list_new(void);

/* Create a new list with one element
 * Time complexity: maximum O(1)
 */
list_t* list_new1(size_t one);

/* Create a new list with two elements
 * Time complexity: maximum O(1)
 */
list_t* list_new2(size_t one, size_t two);

/* Create a new list with three elements
 * Time complexity: maximum O(1)
 */
list_t* list_new3(size_t one, size_t two, size_t three);

/* Create a new list with four elements
 * Time complexity: maximum O(1)
 */
list_t* list_new4(size_t one, size_t two, size_t three, size_t four);

/* Prepend an element to a list
 * Time complexity: maximum O(1)
 */
list_t* list_cons(size_t head, list_t* tail);

/* Get list head (first element)
 * Time complexity: maximum O(1)
 */
size_t list_head(list_t* list);

/* Get list tail (all elements except first)
 * Time complexity: maximum O(1)
 */
list_t* list_tail(list_t* list);

/* Get list element at index
 * Time complexity: maximum O(n)
 */
size_t list_at(list_t* list, size_t n);

/* Get the tail of the list after n elements
 * Time complexity: maximum O(n)
 */
list_t* list_tail_at(list_t* list, size_t n);

/* Get list length
 * Time complexity: maximum O(n) where n is list's length
 */
size_t list_len(list_t* list);

/* Check if the list is empty
 * Time complexity: maximum O(1)
 */
bool list_is_empty(list_t* list);

/* Check if list has at least n elements
 * Time complexity: maximum O(n) where n is the number of elements to check
 */
bool list_has_n(list_t* list, size_t n);

#endif /* FUNCTIONAL_CORE_LIB */
