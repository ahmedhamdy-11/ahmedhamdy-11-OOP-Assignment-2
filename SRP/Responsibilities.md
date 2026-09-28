# SRP Violations & Analysis (Task 1.1)

This document identifies the multiple responsibilities mixed into each of the 10 legacy classes in `SrpLab` and explains why having them together violates the Single Responsibility Principle (SRP).

---

## 1. WardBoard

### Identified Responsibilities
1. **Ward & Patient Management**: Assigning patients to bed numbers and tracking census[cite: 12].
2. **Clinical Acuity Scoring**: Computing the patient acuity score based on heart rate and SpO2 levels[cite: 12].
3. **Emergency Alerting**: Triggering pager codes ("CODE-YELLOW") and managing the pager log[cite: 12].
4. **Handoff Formatting**: Formatting shift-handoff text notes for nurses[cite: 12].
5. **Data Export**: Generating census reports in CSV format[cite: 12].

### Why Having Them Together is a Problem
* **Multiple Reasons to Change**: A change in clinical scoring guidelines, hospital alerting thresholds, or CSV export schemas would all force modifications to this single class[cite: 12].
* **Hidden Side Effects**: Registering a patient automatically triggers side effects (writing to the pager log)[cite: 12].
* **Testing Complexity**: You cannot unit test the patient assignment logic without also testing acuity formulas and alert logging[cite: 12].

---

## 2. CheckoutBasket

### Identified Responsibilities
1. **Cart Item Management**: Storing shopping cart line items, quantities, and calculating the base subtotal[cite: 15].
2. **Marketing Coupon Parsing**: Parsing and validating string-based promotion codes[cite: 15].
3. **Gift Packaging Policy**: Applying gift wrapping charges and fees[cite: 15].
4. **Customer Communication**: Formatting gift card messages for customers[cite: 15].
5. **Payment Processing**: Generating stub authentication payloads for payment gateways[cite: 15].

### Why Having Them Together is a Problem
* **Unrelated Domain Changes**: Marketing coupon rules change frequently, whereas basic shopping cart calculations are static[cite: 15].
* **Security & Integration Risk**: Payment authorization logic is mixed with presentation text formatting, making the class overly complex and prone to regressions[cite: 15].

---

## 3. SupportTicket

### Identified Responsibilities
1. **Ticket Lifecycle & Threading**: Managing support ticket text, appending customer messages, and tracking open timestamps[cite: 21].
2. **Text Priority Analysis (Heuristics)**: Scanning text for keywords to determine ticket priority (P1/P2/P3)[cite: 21].
3. **SLA Management**: Calculating resolution SLA deadlines based on assigned priority[cite: 21].
4. **Customer Communication**: Formatting public reply templates for customers[cite: 21].
5. **Internal Alerting**: Drafting internal escalation summaries[cite: 21].

### Why Having Them Together is a Problem
* **Tangled Logic**: Appending a customer message implicitly triggers keyword scanning and changes the internal state (priority)[cite: 21].
* **Different Stakeholders**: Customer Experience (CX) teams own response templates, while Operations/IT teams own SLA rules and escalation thresholds[cite: 21].

---

## 4. LoanDesk

### Identified Responsibilities
1. **Underwriting Risk Calculation**: Computing numerical risk scores based on credit scores, employment, and collateral[cite: 19].
2. **Eligibility Rules**: Evaluating whether an applicant passes credit approval[cite: 19].
3. **Compliance Checklist**: Determining required regulatory documents[cite: 19].
4. **Decision Notification**: Formatting formal approval or rejection letters[cite: 19].
5. **Reporting/Analytics Export**: Formatting underwriter records into CSV format[cite: 19].

### Why Having Them Together is a Problem
* **Regulatory vs. Business Policy Churn**: Financial compliance rules for document verification change independently of internal risk formulas[cite: 19].
* **Document Formatting**: Changing legal letter phrasing forces changes to underwriting core classes[cite: 19].

---

## 5. CourseEnrollmentDesk

### Identified Responsibilities
1. **Seat Allocation & Capacity**: Managing registered students and waitlist capacity[cite: 16].
2. **Waitlist Promotion Logic**: Processing waitlist promotions when seats become available[cite: 16].
3. **Marketing Onboarding**: Generating Markdown welcome packets with Discord links[cite: 16].
4. **Financial Billing Formatting**: Generating tuition invoice lines with tax/VAT calculations[cite: 16].

### Why Having Them Together is a Problem
* **Finance vs. Operations**: Tax calculations (VAT) and invoice formatting are accounting concerns, not enrollment seat algorithms[cite: 16].
* **Content Churn**: Changes to Discord onboarding links or markdown styling require touching core registration classes[cite: 16].

---

## 6. KitchenTicket

### Identified Responsibilities
1. **Order Item Tracking**: Storing ordered items and prep times[cite: 18].
2. **Allergen Detection**: Scanning ingredients against a regulatory allergen dictionary[cite: 18].
3. **Kitchen Prep Timing**: Calculating estimated kitchen completion time based on station concurrency[cite: 18].
4. **Hardware Ticket Rendering**: Formatting thermal printer text layouts[cite: 18].
5. **Expo Routing**: Determining kitchen lane routing hints[cite: 18].

### Why Having Them Together is a Problem
* **Hardware Coupling**: Thermal printer formatting (text width, layout separators) is tied directly to kitchen operational calculations[cite: 18].
* **Frequent Rules Changes**: Health authority allergen lists change independently of kitchen station algorithms[cite: 18].

---

## 7. SubscriptionBilling

### Identified Responsibilities
1. **Financial Proration**: Calculating prorated subscription amounts for partial billing cycles[cite: 20].
2. **Invoice Numbering**: Managing sequence counters and formatting invoice numbers[cite: 20].
3. **Payment Failure Tracking**: Keeping state of failed payment counts[cite: 20].
4. **Collections Communication**: Drafting dunning email notices with tone severity based on failure count[cite: 20].
5. **Accounting Journal Export**: Formatting ledger lines for accounting integration[cite: 20].

### Why Having Them Together is a Problem
* **Side-Effect Pitfalls**: Generating a dunning email increments invoice sequences as a side effect[cite: 20].
* **Mixed Accounting Rules**: Proration math and collections communications belong to separate business domains (Billing Engine vs. Customer Success/Legal)[cite: 20].

---

## 8. WarehousePickList

### Identified Responsibilities
1. **Stock Allocation Logic**: Calculating item allocation and managing shortages/backorders[cite: 13].
2. **Route Optimization**: Sorting items by aisle and bin to determine physical walking order[cite: 13].
3. **Picker UI Formatting**: Drafting user instructions/scripts for handheld picking devices[cite: 13].
4. **WMS System Integration**: Formatting batch details into WMS XML schema[cite: 13].

### Why Having Them Together is a Problem
* **Hardware & Integration Coupling**: Changes to the handheld device UI or WMS XML contracts force changes to routing logic[cite: 13].
* **Business vs. Operational Rules**: Stock allocation policy changes affect the same class that formats hardware instructions[cite: 13].

---

## 9. GradeBook

### Identified Responsibilities
1. **Score Aggregation**: Storing student scores and calculating averages[cite: 17].
2. **Academic Policy Grading**: Applying letter grade bands (A, B, C, D, F) and Honor Roll qualification[cite: 17].
3. **Transcript Generation**: Formatting plain text academic transcripts for students[cite: 17].
4. **Export Formatting**: Generating CSV reports for administrative registrar systems[cite: 17].

### Why Having Them Together is a Problem
* **Academic Policy vs. Data Storage**: Grading scale policy changes (e.g., changing grade cutoffs) affect the same class that formats CSV exports and plain-text transcripts[cite: 17].

---

## 10. AppointmentDesk

### Identified Responsibilities
1. **Business Hours Rules**: Checking operating hours and weekend policies[cite: 14].
2. **Slot Search & Booking**: Finding available appointment slots and tracking booked appointments[cite: 14].
3. **iCalendar Serialization**: Formatting calendar events into standard ICS format strings[cite: 14].
4. **SMS Messaging**: Drafting SMS appointment reminder messages[cite: 14].

### Why Having Them Together is a Problem
* **Protocol & Channel Coupling**: iCalendar standards (ICS) and SMS text formats change independently of clinic business hours and slot availability rules[cite: 14].
* **Multiple Stakeholders**: Reception staff manage clinic hours, while marketing/telecom manage SMS channel templates[cite: 14].